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
    [Route("api")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly JWTService _jwtService;

        public LoginController(ILoginRepository loginRepository, JWTService jwtService)
        {
            _loginRepository = loginRepository;
            _jwtService = jwtService;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var login = _loginRepository.GetByLoginName(loginDTO.LoginName);

            if (login == null) return BadRequest(new { message = "Invalid Credentials" });

            //LoginName correct password incorrect
            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, login.Password))
            {
                Console.WriteLine($"loginDTO.Password: {loginDTO.Password}");
                Console.WriteLine($"login.Password: {login.Password}");

                return BadRequest(new { message = "Invalid Password" });
            }

            var jwt = _jwtService.GenerateSecurityToken(login.LoginName);

            //Cookie küldése tokenben
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "success",
                token = jwt
            });
        }

            [HttpGet("getLogin")]
            public IActionResult Login()
            {
                try
                {
                    var jwt = Request.Cookies["jwt"];

                    var token = _jwtService.Verify(jwt);

                    var loginID = int.Parse(token.Issuer);

                    var login = _loginRepository.GetByID(loginID);


                    return Ok(login);
                } catch (Exception _)
                {
                    return Unauthorized();
                }
            }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");


            return Ok(new { message = "logout success" });
        }

        
    }
}

