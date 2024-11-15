using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sol_server_api.Data;
using Sol_server_api.DTOs;
using Sol_server_api.Entities;
using Sol_server_api.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Sol_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoworkerController : ControllerBase
    {
        private readonly SolContext _context;
        private readonly ILoginRepository _loginRepository;
        private readonly JWTService _jwtService;
        private readonly AuthorizationService _authorizationService;

        public CoworkerController(SolContext context, ILoginRepository loginRepository, JWTService jwtService, AuthorizationService authorizationService)
        {
            _context = context;
            _loginRepository = loginRepository;
            _jwtService = jwtService;
            _authorizationService = authorizationService;

        }

        // GET: api/Coworker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coworker>>> GetCoworkers()
        {
            var jwt = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(jwt))
            {
                return Unauthorized("JWT token is missing.");
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var adminName = token.Issuer;

                if (!_authorizationService.adminHasPermission(adminName, "GET_COWORKER"))
                {
                    return Forbid();
                }

                var coworkers = await _context.Coworkers
                    .Include(c => c.PersonalData)
                    .Include(c => c.Login)
                    .ToListAsync();

                return Ok(coworkers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/Coworker/search?name=John
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Coworker>>> SearchCoworkers(string name)
        {
            var jwt = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(jwt))
            {
                return Unauthorized("JWT token is missing.");
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var adminName = token.Issuer;

                if (!_authorizationService.adminHasPermission(adminName, "GET_COWORKER"))
                {
                    return Forbid();
                }

                var coworkers = await _context.Coworkers
                    .Include(c => c.PersonalData)
                    .Include(c => c.Login)
                    .Where(c => c.CoworkerName.Contains(name))
                    .ToListAsync();

                return Ok(coworkers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/Coworker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coworker>> GetCoworker(string id)
        {
            var coworker = await _context.Coworkers.FindAsync(id);

            if (coworker == null)
            {
                return NotFound();
            }

            return coworker;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Coworker>> PostCoworker([FromBody] CoworkerDTO coworkerDTO)
        {
            var jwt = Request.Headers["Authorization"]; // JWT token from request cookies

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from cookies.");
                return Unauthorized("JWT token is missing."); // Return 401 if JWT is missing
            }

            try
            {
                var token = _jwtService.Verify(jwt);  // Verify and decode the JWT
                var adminName = token.Issuer;  // Extract admin ID from token issuer field

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"AdminID: {adminName}");

                // Check if the admin has permission to create a new coworker
                if (!_authorizationService.adminHasPermission(adminName, "CREATE_COWORKER"))
                {
                    return Forbid();  // Return 403 if admin does not have permission
                }

                var coworker = new Coworker
                {
                    CoworkerName = coworkerDTO.CoworkerName,
                    RoleID = coworkerDTO.RoleID,
                    Login = new Login
                    {
                        LoginName = coworkerDTO.Login.LoginName,
                        Password = BCrypt.Net.BCrypt.HashPassword(coworkerDTO.Login.Password),
                        FKLoginCWID = coworkerDTO.CoworkerID
                    }
                };

                _context.Coworkers.Add(coworker);
                await _context.SaveChangesAsync();

                var personalData = new PersonalData
                {
                    TelNumber = coworkerDTO.PersonalData.TelNumber,
                    Email = coworkerDTO.PersonalData.Email,
                    Address = coworkerDTO.PersonalData.Address,
                    CoworkerID = coworker.CoworkerID
                };

                _context.PersonalDatas.Add(personalData);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCoworker), new { id = coworker.CoworkerID }, coworker);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Coworker/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCoworker([FromBody] UpdateCoworkerDTO updateDTO)
        {
            /*try
            {*/
                var jwt = Request.Headers["Authorization"]; // JWT token a kérés fejlécéből

                if (string.IsNullOrEmpty(jwt))
                {
                    return Unauthorized("Hiányzik a JWT token.");
                }

                var token = _jwtService.Verify(jwt);  // JWT token verifikálása és dekódolása
                var adminName = token.Issuer;  // Az adminisztrátor azonosítója a tokenből

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"AdminID: {adminName}");

                // Ellenőrzés, hogy az adminisztrátornak van-e jogosultsága munkatársak frissítésére
                if (!_authorizationService.adminHasPermission(adminName, "UPDATE_COWORKER"))
                {
                    return Forbid();  // 403 hibakód visszaadása, ha az adminisztrátornak nincs jogosultsága
                }

           

            // Munkatárs keresése az email alapján
            var coworker = await _context.Coworkers
                                            .Include(c => c.Login)
                                            .Include(c => c.PersonalData)
                                            .FirstOrDefaultAsync(c => c.PersonalData.Email == updateDTO.PersonalData.Email);

                if (coworker == null)
                {
                    return NotFound("A munkatárs nem található");
                }

                // Csak azokat a mezőket frissítjük, amiket a frontend elküldött

                // Munkatárs nevének frissítése, ha meg van adva
                if (!string.IsNullOrEmpty(updateDTO.CoworkerName))
                {
                    coworker.CoworkerName = updateDTO.CoworkerName;
                }

                // Szerepkör frissítése, ha meg van adva
                if (updateDTO.RoleID.HasValue)
                {
                    coworker.RoleID = updateDTO.RoleID.Value;
                }

                // Login adatok frissítése, ha meg vannak adva
                if (updateDTO.Login != null)
                {
                    if (!string.IsNullOrEmpty(updateDTO.Login.LoginName))
                    {
                        coworker.Login.LoginName = updateDTO.Login.LoginName;
                    }
                    if (!string.IsNullOrEmpty(updateDTO.Login.Password))
                    {
                        coworker.Login.Password = BCrypt.Net.BCrypt.HashPassword(updateDTO.Login.Password);
                    }
                }

                // Személyes adatok frissítése, ha meg vannak adva
                if (updateDTO.PersonalData != null)
                {
                    if (!string.IsNullOrEmpty(updateDTO.PersonalData.TelNumber))
                    {
                        coworker.PersonalData.TelNumber = updateDTO.PersonalData.TelNumber;
                    }
                    if (!string.IsNullOrEmpty(updateDTO.PersonalData.Email))
                    {
                        coworker.PersonalData.Email = updateDTO.PersonalData.Email;
                    }
                    if (!string.IsNullOrEmpty(updateDTO.PersonalData.Address))
                    {
                        coworker.PersonalData.Address = updateDTO.PersonalData.Address;
                    }
                }

                _context.Entry(coworker).State = EntityState.Modified;  // Az entity állapotának módosítása
                await _context.SaveChangesAsync();  // Változások mentése az adatbázisba

                return Ok(coworker);  // Visszaadás: frissített munkatárs objektum
            /*}
            catch (Exception ex)
            {
                return StatusCode(500, $"Belső szerver hiba: {ex.Message}");  // Hibakezelés: 500-as hibakód visszaadása
            }*/
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCoworker(int id)
        {
            var jwt = Request.Headers["Authorization"]; // JWT token from request cookies

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from cookies.");
                return Unauthorized("JWT token is missing."); // Return 401 if JWT is missing
            }

            try
            {
                var token = _jwtService.Verify(jwt);  // Verify and decode the JWT
                var adminName = token.Issuer;  // Extract admin ID from token issuer field

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"AdminID: {adminName}");

                // Check if the admin has permission to delete a coworker
                if (!_authorizationService.adminHasPermission(adminName, "DELETE_COWORKER"))
                {
                    return Forbid();  // Return 403 if admin does not have permission
                }

                // Find the coworker by ID
                var coworker = await _context.Coworkers
                    .Include(c => c.Login)
                    .Include(c => c.PersonalData)
                    .FirstOrDefaultAsync(c => c.CoworkerID == id);

                if (coworker == null)
                {
                    return NotFound("Coworker not found.");
                }

                // Remove associated Login and PersonalData
                if (coworker.Login != null)
                {
                    _context.Logins.Remove(coworker.Login);
                }
                if (coworker.PersonalData != null)
                {
                    _context.PersonalDatas.Remove(coworker.PersonalData);
                }

                // Remove the coworker
                _context.Coworkers.Remove(coworker);

                // Save changes to the database
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        private bool CoworkerExists(int id)
        {
            return _context.Coworkers.Any(e => e.CoworkerID == id);
        }
    }
}
