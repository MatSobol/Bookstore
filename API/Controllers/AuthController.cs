using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Services.AuthService;
using SharedP;
using SharedP.Auth;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpGet("loginGoogle")]
        public async Task<IActionResult> LoginGoogle2(string requestId)
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var response = await _authService.Login("guess@gmail.com", "string");
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                if(requestId != "2")
                {
                    return Redirect("http://localhost:5046/products/bookList?token=" + response.Data);
                }
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><body><h5>"+response.Data+"</h5></body></html>"
                };
            }
            else
            {
                return Challenge(GoogleDefaults.AuthenticationScheme);
            }
        }
        [HttpPost("GoogleCallback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                return Ok("Home");
            }

            return Ok("Login");
        }

        [HttpGet("loginMicrosoft")]
        public async Task<IActionResult> LoginMicrosoft(string? requestId)
        {
            var result = await HttpContext.AuthenticateAsync(MicrosoftAccountDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var response = await _authService.Login("guess@gmail.com", "string");
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                if (requestId != "2")
                {
                    return Redirect("http://localhost:5046/products/bookList?token=" + response.Data);
                }
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><body><h5>" + response.Data + "</h5></body></html>"
                };
            }
            else
            {
                return Challenge(MicrosoftAccountDefaults.AuthenticationScheme);
            }
        }
        [HttpPost("MicrosoftCallback")]
        public async Task<IActionResult> MicrosoftCallback()
        {
            var result = await HttpContext.AuthenticateAsync(MicrosoftAccountDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                return Ok("Home");
            }

            return Ok("Login");
        }

        [HttpGet("LoginFacebook")]
        public async Task<IActionResult> LoginFacebook(string? requestId)
        {
            var result = await HttpContext.AuthenticateAsync(FacebookDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var response = await _authService.Login("guess@gmail.com", "string");
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                if (requestId != "2")
                {
                    return Redirect("http://localhost:5046/products/bookList?token=" + response.Data);
                }
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><body><h5>" + response.Data + "</h5></body></html>"
                };
            }
            else
            {
                return Challenge(FacebookDefaults.AuthenticationScheme);
            }
        }
        [HttpPost("FacebookCallback")]
        public async Task<IActionResult> FacebookCallback()
        {
            var result = await HttpContext.AuthenticateAsync(FacebookDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                return Ok("Home");
            }

            return Ok("Login");
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDTO userLoginDTO)
        {
            var response = await  _authService.Login(userLoginDTO.Email, userLoginDTO.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO userRegisterDTO)
        {
            var user = new User()
            {
                Email = userRegisterDTO.Email,
                Username = userRegisterDTO.Username
            };

            var response = await _authService.Register(user, userRegisterDTO.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePassword(int.Parse(userId), newPassword);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
