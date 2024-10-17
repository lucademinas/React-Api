using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(IConfiguration configuration, ICustomAuthenticationService customAuthenticationService)
        {
            _configuration = configuration; //Hacemos la inyección para poder usar el appsettings.json
            _customAuthenticationService = customAuthenticationService;
        }

        [HttpPost]
        public ActionResult<string> Login(LoginRequest loginRequest)
        {
            var token = _customAuthenticationService.Login(loginRequest);
            if(string.IsNullOrEmpty(token))
                return StatusCode(401);
            return Ok(token);    
        }
    }
}
