using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sol_server_api.Data;
using Sol_server_api.Entities;
using Sol_server_api.DTOs;
using Sol_server_api.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Sol_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly SolContext _context;
        private readonly ILoginRepository _loginRepository;
        private readonly JWTService _jwtService;
        private readonly AuthorizationService _authorizationService;

        public ProjectController(SolContext context, ILoginRepository loginRepository, JWTService jwtService, AuthorizationService authorizationService)
        {
            _context = context;
            _jwtService = jwtService;
            _loginRepository = loginRepository;
            _authorizationService = authorizationService;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProject()
        {
            
            // Ellenőrizzük, hogy van-e Authorization fejléc a kérésben
            var jwt = Request.Headers["Authorization"]; // JWT token formátum: "Bearer token"

            if (string.IsNullOrEmpty(jwt))
            {
                return Unauthorized("JWT token is missing.");
            }

            try
            {
                // Token ellenőrzése
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                // Jogosultság ellenőrzése
                if (!_authorizationService.UserHasPermission(loginName, "Projektek listázása"))
                {
                    return Forbid();
                }

                // Projekt adatok lekérdezése az adatbázisból
                var projects = await _context.Projects.ToListAsync();

                return Ok(projects);
            }
            
            catch (Exception ex)
            {
                // Hiba kezelés
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
            
        }


        [HttpPost("post")]
        public async Task<ActionResult<Project>> PostProject ([FromBody] ProjectDTO projectDTO)
        {
            var jwt = Request.Headers["Authorization"];
            
            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from cookies.");
                return Unauthorized("JWT token is missing."); // Return 401 if JWT is missing
            }

            try
            {
                var token = _jwtService.Verify(jwt);  // Verify and decode the JWT
                var loginName = token.Issuer;  // Extract admin ID from token issuer field

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"AdminID: {loginName}");

                if (!_authorizationService.UserHasPermission(loginName, "Új projekt létrehozása"))
                {
                    return Forbid("Nincs jogosultsága új projekt létrehozásához.");
                }

                var project = new Project
                {
                    Location = projectDTO.Location,
                    ProjectDate = projectDTO.ProjectDate,
                    Description = projectDTO.Description,
                    ProcessStatus = projectDTO.ProcessStatus,
                    FKCustomerID = projectDTO.FKCustomerID,
                    FKCoworkerID = projectDTO.FKCoworkerID
                };

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetProject), new { id = project.ProjectID }, project);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{coworkerId}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByCoworker(int coworkerId)
        {
            var jwt = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(jwt))
            {
                return Unauthorized("JWT token is missing.");
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                // Jogosultság ellenőrzése
                if (!_authorizationService.UserHasPermission(loginName, "Projektek listázása"))
                {
                    return Forbid();
                }

                // Szűrés a CoworkerID alapján
                var projects = await _context.Projects
                    .Where(p => p.FKCoworkerID == coworkerId)
                    .ToListAsync();

                if (projects == null || projects.Count == 0)
                {
                    return NotFound($"No projects found for coworker ID {coworkerId}");
                }

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{projectId}/components")]
        public async Task<ActionResult<ProjectPackageDTO>> GetProjectComponents(int projectId)
        {
           /* var jwt = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token hiányzik a fejlécből.");
                return Unauthorized("JWT token is missing.");
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                if (!_authorizationService.UserHasPermission(loginName, "Projekthez tartozó alkatrészek listázása"))
                {
                    return Forbid("Nincs jogosultsága a projekthez tartozó alkatrészek megtekintéséhez.");
                }*/

                var projectPackage = await _context.ProjectPackages
                    .Include(p => p.PackageComponents)
                    .Where(p => p.FKProjectID == projectId)  // Csak az adott projekt ID-hoz tartozó csomagokat kérdezzük le
                    .ToListAsync();

                if (projectPackage == null || !projectPackage.Any())
                {
                    return NotFound("Nincsenek alkatrészek a megadott projekthez.");
                }

                var projectPackagesDto = projectPackage.Select(pp => new ProjectPackageDTO
                {
                    RequiredComponentName = pp.RequiredComponentName,
                    ForDelivery = pp.ForDelivery,
                    FKProjectID = pp.FKProjectID,
                    PackageComponents = pp.PackageComponents.Select(pc => new PackageComponentDTO
                    {
                        ComponentName = pc.ComponentName,
                        RequiredPiece = pc.RequiredPiece,
                        RealPiece = pc.RealPiece
                    }).ToList()
                }).ToList();

                return Ok(projectPackagesDto);
            /*}
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }*/
        }



        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProject(int id, [FromBody] ProjectUpdateDTO projectUpdateDTO)
        {
            var existingProject = await _context.Projects
                .Include(p => p.Coworker) // Include other necessary entities
                .FirstOrDefaultAsync(p => p.ProjectID == id);

            if (existingProject == null)
            {
                return NotFound();
            }

            // Update only the fields that are provided in the DTO
            if (projectUpdateDTO.Location != null)
                existingProject.Location = projectUpdateDTO.Location;

            if (projectUpdateDTO.ProjectDate.HasValue)
                existingProject.ProjectDate = projectUpdateDTO.ProjectDate.Value;

            if (projectUpdateDTO.Description != null)
                existingProject.Description = projectUpdateDTO.Description;

            if (projectUpdateDTO.ProcessStatus != null)
                existingProject.ProcessStatus = projectUpdateDTO.ProcessStatus;

            if (projectUpdateDTO.FKCustomerID.HasValue)
            {
                // Check if FKCustomerID is valid if necessary
                existingProject.FKCustomerID = projectUpdateDTO.FKCustomerID.Value;
            }

            if (projectUpdateDTO.FKCoworkerID.HasValue)
            {
                // Check if FKCoworkerID is valid if necessary
                existingProject.FKCoworkerID = projectUpdateDTO.FKCoworkerID.Value;
            }

            _context.Entry(existingProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        //Process status
        [HttpPatch("Process/{id}")]
        public async Task<IActionResult> UpdateProjectStatus(int id, [FromBody] ProjectDTO projectDTO)
        {
            var jwt = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(jwt))
            {
                return Unauthorized("JWT token is missing.");
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                // Jogosultság ellenőrzése
                if (!_authorizationService.UserHasPermission(loginName, "Projekt lezárása"))
                {
                    return Forbid();
                }
                var project = await _context.Projects.FindAsync(id);
                if (project == null)
                {
                    return NotFound();
                }

                project.ProcessStatus = projectDTO.ProcessStatus;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Státusz sikeresen frissítve!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update-status/{projectId}")]
        public async Task<IActionResult> UpdateProjectStatus(int projectId)
        {
            var jwt = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from cookies.");
                return Unauthorized("JWT token is missing.");
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                if (!_authorizationService.UserHasPermission(loginName, "Projekt kiválasztása kivételezéshez"))
                {
                    return Forbid("Nincs jogosultsága a projekt státuszának frissítéséhez.");
                }

                var project = await _context.Projects.FindAsync(projectId);

                if (project == null)
                {
                    return NotFound("Projekt nem található.");
                }

                project.ProcessStatus = "InProgress";
                await _context.SaveChangesAsync();

                return Ok("A projekt státusza sikeresen frissítve.");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
                return StatusCode(500, "Hiba történt a frissítés során.");
            }
        }



        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Coworker)
                .FirstOrDefaultAsync(p => p.ProjectID == id);

            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectID == id);
        }

    }
}
