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
    public class ComponentController : ControllerBase
    {
        private readonly SolContext _context;
        private readonly ILoginRepository _loginRepository;
        private readonly JWTService _jwtService;
        private readonly AuthorizationService _authorizationService;

        public ComponentController(SolContext context, ILoginRepository loginRepository, JWTService jwtService, AuthorizationService authorizationService)
        {
            _context = context;
            _loginRepository = loginRepository;
            _jwtService = jwtService;
            _authorizationService = authorizationService;
        }

        // GET: api/Component
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentDTO>>> GetComponents()
        {
            var jwt = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from headers.");
                return Unauthorized("JWT token is missing."); // 401-es válasz
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"User: {loginName}");

                if (!_authorizationService.UserHasPermission(loginName, "Alkatrészek listázása"))
                {
                    return Forbid("Nincs jogosultsága megtekinteni az alkatrészeket."); // 403-as válasz
                }

                var components = await _context.Components.Select(c => new ComponentDTO
                {
                    ComponentID = c.ComponentID,
                    ComponentName = c.ComponentName,
                    Price = c.Price,
                    StockStatus = c.StockStatus,
                    FKPackageID = c.FKPackageID,
                    CompartmentID = c.CompartmentID
                }).ToListAsync();

                return Ok(components);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error"); // 500-as válasz
            }
        }

        // GET: api/Component/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentDTO>> GetComponent(int id)
        {
            var jwt = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from headers.");
                return Unauthorized("JWT token is missing."); // 401-es válasz
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"User: {loginName}");

                if (!_authorizationService.UserHasPermission(loginName, "Alkatrészek listázása"))
                {
                    return Forbid("Nincs jogosultsága megtekinteni az alkatrészt."); // 403-as válasz
                }

                var component = await _context.Components.FindAsync(id);
                if (component == null)
                {
                    return NotFound("Component not found."); // 404-es válasz
                }

                var componentDto = new ComponentDTO
                {
                    ComponentID = component.ComponentID,
                    ComponentName = component.ComponentName,
                    Price = component.Price,
                    StockStatus = component.StockStatus,
                    FKPackageID = component.FKPackageID,
                    CompartmentID = component.CompartmentID
                };

                return Ok(componentDto);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error"); // 500-as válasz
            }
        }

        // PUT: api/Component/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Component component)
        {
            if (id != component.ComponentID)
            {
                return BadRequest();
            }

            _context.Entry(component).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
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

        // POST: api/Component
        [HttpPost("post")]
        public async Task<ActionResult<Component>> PostComponent([FromBody] ComponentDTO componentDTO)
        {
            // Kérés fejléceiből JWT token kiolvasása
            var jwt = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from headers.");
                return Unauthorized("JWT token is missing."); // 401-es válasz, ha nincs JWT
            }

            try {
                // JWT ellenőrzése
                var token = _jwtService.Verify(jwt);  // Verify and decode the JWT
                var loginName = token.Issuer;  // Extract user ID from token issuer field

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"User: {loginName}");

                // Jogosultság ellenőrzése
                if (!_authorizationService.UserHasPermission(loginName, "Új alkatrészek felvétele a rendszerbe"))
                {
                    return Forbid("Nincs jogosultsága új alkatrészt hozzáadni az adatbázishoz."); // 403 - Forbidden
                }

               

                // DTO konvertálása entitássá
                var component = new Component
                {
                    ComponentName = componentDTO.ComponentName,
                    Price = componentDTO.Price,
                    StockStatus = componentDTO.StockStatus,
                    FKPackageID = componentDTO.FKPackageID,
                    CompartmentID = componentDTO.CompartmentID
                };

                // Entitás hozzáadása az adatbázishoz
                _context.Components.Add(component);
                await _context.SaveChangesAsync();

                // Újonnan létrehozott erőforrás visszaadása Created státusszal
                return CreatedAtAction(nameof(GetComponent), new { id = component.ComponentID }, component);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error"); // 500 - Internal Server Error
            }
        }

        // POST: api/Component/Prices
        [HttpPost("Prices")]
        public async Task<ActionResult> GetComponentPrices([FromBody] List<string> componentNames)
        {
            var jwt = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from headers.");
                return Unauthorized("JWT token is missing."); // 401-es válasz
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                if (!_authorizationService.UserHasPermission(loginName, "Becsült munkavégzési idő rögzítése"))
                {
                    return Forbid("Nincs jogosultsága megtekinteni az alkatrészeket."); // 403-as válasz
                }

                // Alkatrészek keresése az adatbázisban a nevek alapján
                var components = await _context.Components
                    .Where(c => componentNames.Contains(c.ComponentName))
                    .Select(c => new { c.ComponentName, c.Price })
                    .ToListAsync();

                if (!components.Any())
                {
                    return NotFound("Nem találhatóak az adott nevű alkatrészek.");
                }

                // Visszaadja az árakat
                var prices = components.Select(c => c.Price).ToList();
                return Ok(new { prices });
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        // DELETE: api/Component/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(string id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComponentExists(int id)
        {
            return _context.Components.Any(e => e.ComponentID == id);
        }
    }
}