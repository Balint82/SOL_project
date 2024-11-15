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
    public class CustomerController : ControllerBase
    {
        private readonly SolContext _context;
        private readonly ILoginRepository _loginRepository;
        private readonly JWTService _jwtService;
        private readonly AuthorizationService _authorizationService;

        public CustomerController(SolContext context, ILoginRepository loginRepository, JWTService jWTService, AuthorizationService authorizationService)
        {
            _context = context;
            _loginRepository = loginRepository;
            _jwtService = jWTService;
            _authorizationService = authorizationService;  
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
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

                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                {
                    return NotFound();
                }

                return customer;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


            // PUT: api/Customer/5
            [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customer
        [HttpPost("register")]
        public async Task<ActionResult<Project>> PostCustomer([FromBody] CustomerDTO customerDto)
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

                var customer = new Customer
                {
                    CustomerName = customerDto.CustomerName,
                    CustomerEmail = customerDto.CustomerEmail,
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customer);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error verifying JWT: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}




