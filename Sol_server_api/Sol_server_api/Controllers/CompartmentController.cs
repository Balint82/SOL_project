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

namespace Sol_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompartmentController : ControllerBase
    {
        private readonly SolContext _context;
        private readonly ILoginRepository _loginRepository;
        private readonly JWTService _jwtService;
        private readonly AuthorizationService _authorizationService;


        public CompartmentController(SolContext context, ILoginRepository loginRepository, JWTService jwtService, AuthorizationService authorizationService)
        {
            _context = context;
            _loginRepository = loginRepository;
            _jwtService = jwtService;
            _authorizationService = authorizationService;
        }

        // GET: api/Compartment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compartment>>> GetCompartments()
        {
            return await _context.Compartments.ToListAsync();
        }

        // GET: api/Compartment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compartment>> GetCompartment(string id)
        {
            var compartment = await _context.Compartments.FindAsync(id);

            if (compartment == null)
            {
                return NotFound();
            }

            return compartment;
        }


        [HttpGet("zero-stock")]
        public async Task<ActionResult<IEnumerable<Component>>> GetComponentsWithZeroStock()
        {
            // JWT ellenőrzés
            var jwt = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(jwt))
            {
                return Unauthorized("JWT token hiányzik.");
            }

            try
            {
                // Token dekódolása és validálása
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                // Jogosultság ellenőrzése (opcionális)
                if (!_authorizationService.UserHasPermission(loginName, "Hiányzó alkatrészek listázása"))
                {
                    return Forbid("Nincs jogosultsága az alkatrészek megtekintéséhez.");
                }

                var compartments = await _context.Compartments.Where(c => c.StoragedPiece == 0 || c.StoragedPiece == null).ToListAsync();

                return Ok(compartments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt az adatok lekérése közben: {ex.Message}");
            }
        }

        [HttpGet("check-stock/{componentName}")]
        public async Task<ActionResult<Compartment>> CheckStock(string componentName)
        {
            var compartment = await _context.Compartments
                .FirstOrDefaultAsync(c => c.StoragedComponentName == componentName);

            if (compartment == null)
                return NotFound("Alkatrész nem található.");

            return Ok(compartment);
        }


        // PUT: api/Compartment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompartment(int id, Compartment compartment)
        {
            if (id != compartment.CompartmentID)
            {
                return BadRequest();
            }

            _context.Entry(compartment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompartmentExists(id))
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

        [HttpPost("post")]
        public async Task<ActionResult<Compartment>> PostCompartment([FromBody] CompartmentDTO compartmentDTO)
        {
            var jwt = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(jwt))
            {
                await Console.Out.WriteLineAsync("JWT token is missing from headers.");
                return Unauthorized("JWT token is missing.");
            }

            try
            {
                var token = _jwtService.Verify(jwt);
                var loginName = token.Issuer;

                await Console.Out.WriteLineAsync($"JWT: {jwt}");
                await Console.Out.WriteLineAsync($"Token: {token}");
                await Console.Out.WriteLineAsync($"AdminID: {loginName}");

                if (!_authorizationService.UserHasPermission(loginName, "Beérkező anyagok felvétele"))
                {
                    return Forbid("Nincs jogosultsága az alkatrészt rekeszhez hozzárendelni.");
                }

                // Ellenőrizzük, hogy létezik-e már ilyen alkatrész a Compartment táblában
                var existingCompartment = await _context.Compartments
                    .FirstOrDefaultAsync(c => c.StoragedComponentName == compartmentDTO.StoragedComponentName);

                if (existingCompartment != null)
                {
                    // Ha létezik, akkor a StoragedPiece mezőhöz hozzáadjuk az új mennyiséget
                    existingCompartment.StoragedPiece = (existingCompartment.StoragedPiece ?? 0) + compartmentDTO.StoragedPiece;

                    _context.Compartments.Update(existingCompartment);
                    await _context.SaveChangesAsync();

                    return Ok(new {message = "Az alkatrészből van tárolva, rekeszben tárolt mennyiség frissült"}); // Visszatérünk az újonnan frissített adatokkal
                }
                else
                {
                    var compartment = new Compartment
                    {
                        StoragedComponentName = compartmentDTO.StoragedComponentName,
                        Row = compartmentDTO.Row,
                        Col = compartmentDTO.Col,
                        Bracket = compartmentDTO.Bracket,
                        MaximumPiece = compartmentDTO.MaximumPiece,
                        StoragedPiece = compartmentDTO.StoragedPiece,
                        FKComponentID = compartmentDTO.FKComponentID
                    };

                    _context.Compartments.Add(compartment);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetCompartment), new { id = compartment.CompartmentID }, compartment);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the compartment.");
            }
        }

        // DELETE: api/Compartment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompartment(string id)
        {
            var compartment = await _context.Compartments.FindAsync(id);
            if (compartment == null)
            {
                return NotFound();
            }

            _context.Compartments.Remove(compartment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompartmentExists(int id)
        {
            return _context.Compartments.Any(e => e.CompartmentID == id);
        }
    }
}