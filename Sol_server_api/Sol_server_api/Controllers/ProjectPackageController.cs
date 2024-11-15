// CustomerController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sol_server_api.Data;
using Sol_server_api.DTOs;
using Sol_server_api.Entities;
using Sol_server_api.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectPackageController : ControllerBase
    {
        private readonly SolContext _context;
        private readonly ILoginRepository _loginRepository;
        private readonly JWTService _jwtService;
        private readonly AuthorizationService _authorizationService;

        public ProjectPackageController(SolContext context, ILoginRepository loginRepository, JWTService jWTService, AuthorizationService authorizationService)
        {
            _context = context;
            _loginRepository = loginRepository;
            _jwtService = jWTService;
            _authorizationService = authorizationService;
        }

        // GET: api/ProjectPackage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectPackage>>> GetProjectPackages()
        {
            return await _context.ProjectPackages.ToListAsync();
        }

        // GET: api/ProjectPackage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectPackageDTO>> GetProjectPackage(int id)
        {
            // JWT token lekérése a kérés fejlécéből
            var jwt = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token hiányzik a fejlécből.");
                return Unauthorized("JWT token is missing."); // 401-es hiba, ha a token hiányzik
            }

            try
            {
                // JWT token ellenőrzése és dekódolása
                var token = _jwtService.Verify(jwt);  // Verify and decode the JWT
                var loginName = token.Issuer;  // A bejelentkezett felhasználó azonosítója

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"LoginName: {loginName}");

                // Jogosultság ellenőrzése
                if (!_authorizationService.UserHasPermission(loginName, "Projekthez tartozó alkatrészek listázása"))
                {
                    return Forbid("Nincs jogosultsága a csomagok megtekintéséhez."); // 403-as hiba, ha nincs jogosultság
                }

                // Csomag és kapcsolódó komponensek lekérése
                var projectPackage = await _context.ProjectPackages
                    .Include(p => p.PackageComponents)  // Kapcsolódó komponensek betöltése
                    .FirstOrDefaultAsync(p => p.ProjectPackageID == id);

                if (projectPackage == null)
                {
                    return NotFound("A megadott ID-val nem található csomag."); // 404-es hiba, ha nincs találat
                }

                // Az adatokat DTO-ba konvertáljuk
                var projectPackageDto = new ProjectPackageDTO
                {
                    RequiredComponentName = projectPackage.RequiredComponentName,
                    ForDelivery = projectPackage.ForDelivery,
                    FKProjectID = projectPackage.FKProjectID,
                    PackageComponents = projectPackage.PackageComponents.Select(pc => new PackageComponentDTO
                    {
                        ComponentName = pc.ComponentName,
                        RequiredPiece = pc.RequiredPiece,
                        RealPiece = pc.RealPiece
                    }).ToList()
                };

                return Ok(projectPackageDto);
            }
            catch (Exception ex)
            {
                // Hibakezelés: hiba naplózása és visszaadása a kliensnek
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        // PUT: api/ProjectPackage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectPackage(int id, ProjectPackage projectPackage)
        {
            if (id != projectPackage.ProjectPackageID)
            {
                return BadRequest();
            }

            _context.Entry(projectPackage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectPackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProjectPackage
        [HttpPost("create-package")]
        public async Task<ActionResult<ProjectPackage>> PostProjectPackage([FromBody] ProjectPackageDTO projectPackageDto)
        {
            // JWT token lekérése a kérés fejlécéből
            var jwt = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from headers.");
                return Unauthorized("JWT token is missing."); // Return 401 if JWT is missing
            }

            try
            {
                // JWT token ellenőrzése és dekódolása
                var token = _jwtService.Verify(jwt);  // Verify and decode the JWT
                var loginName = token.Issuer;  // Kinyerjük az admin ID-t a token issuer mezőjéből

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"LoginName: {loginName}");

                // Jogosultság ellenőrzése
                if (!_authorizationService.UserHasPermission(loginName, "Alkatrészek projekthez rendelése"))
                {
                    return Forbid("Nincs jogosultsága projektcsomag létrehozásához.");
                }

                var projectPackage = new ProjectPackage
                {
                    RequiredComponentName = projectPackageDto.RequiredComponentName,
                    ForDelivery = projectPackageDto.ForDelivery,
                    FKProjectID = projectPackageDto.FKProjectID,
                    // Kapcsolódó komponensek hozzáadása
                    PackageComponents = projectPackageDto.PackageComponents.Select(c => new PackageComponent
                    {
                        ComponentName = c.ComponentName,
                        RequiredPiece = c.RequiredPiece,
                        RealPiece = c.RealPiece
                    }).ToList()
                };

                _context.ProjectPackages.Add(projectPackage);
                await _context.SaveChangesAsync();

                // Levonjuk a valós mennyiségeket a raktárból
                foreach (var component in projectPackageDto.PackageComponents)
                {
                    var compartment = await _context.Compartments
                        .FirstOrDefaultAsync(c => c.StoragedComponentName == component.ComponentName);

                    if (compartment != null)
                    {
                        compartment.StoragedPiece -= component.RealPiece;
                        _context.Compartments.Update(compartment);
                    }
                }

                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProjectPackage), new { id = projectPackage.ProjectPackageID }, projectPackage);
                }
            catch (Exception ex)
            {
                // Hibakezelés: naplózás és hibaüzenet visszaadása
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        // DELETE: api/ProjectPackage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectPackage(string id)
        {
            var projectPackage = await _context.ProjectPackages.FindAsync(id);
            if (projectPackage == null)
            {
                return NotFound();
            }

            _context.ProjectPackages.Remove(projectPackage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectPackageExists(int id)
        {
            return _context.ProjectPackages.Any(e => e.ProjectPackageID == id);
        }
    }
}




