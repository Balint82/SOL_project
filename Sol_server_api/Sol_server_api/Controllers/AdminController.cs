using Microsoft.AspNetCore.Mvc;
using Sol_server_api.Data;
using Sol_server_api.Helpers;
using Sol_server_api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Sol_server_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly SolContext _context;
        private readonly JWTService _jwtService;

        public AdminController(SolContext context, JWTService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("admin-login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginReq loginRequest)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(adm => adm.AdminName == loginRequest.AdminName);

            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, admin.Password))
            {
                return Unauthorized();
            }

            var token = _jwtService.GenerateSecurityToken(admin.AdminName);
            return Ok(new { token });
        }


        [HttpPost("admin-logout")]
        public IActionResult Logout()
        {
            // Törlés a response cookie-ból (pl. "jwt" néven)
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Admin logout success" });
        }


        
        [HttpGet("verifyToken")]
        public IActionResult VerifyToken()
        {
            // Ellenőrizd a JWT tokent itt és adja vissza a választ
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Unauthorized(new { success = false });
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var validatedToken = _jwtService.Verify(token);

            if (validatedToken == null)
            {
                return Unauthorized(new { success = false });
            }

            // Ellenőrizd, hogy a token tulajdonosa jogosult-e az adminisztrációs funkciókhoz
            var isAdmin = IsAdminUser(validatedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
            if (!isAdmin)
            {
                return Unauthorized(new { success = false });
            }

            return Ok(new { success = true });
        }

        private bool IsAdminUser(string adminName)
        {
            // Ellenőrizd itt, hogy az adott felhasználói név (adminName) jogosult-e az adminisztrációs funkciókhoz
            // Például adatbázisból lekérdezés vagy egyéb megfelelő validációs mechanizmus alkalmazása
            // Példakód:
            var isAdmin = _context.Admins.Any(adm => adm.AdminName == adminName);
            return isAdmin;
        }

    }
}