using Microsoft.AspNetCore.Mvc;
using TrelloC.Authorization;
using TrelloC.Models;
using TrelloC.Services;

namespace TrelloC.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var responce = _userService.Authenticate(model, ipAddress());
            setTokenCookie(responce.RefreshToken);
            return Ok(responce);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            // accept refresh token in request body or cookie - принять токен обновления в теле запроса или файле cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            _userService.RevokeToken(token, ipAddress());
            return Ok(new {message = "Token revoked" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var user = _userService.GetAll();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        //helper methods

        private void setTokenCookie(string token)
        {
            // apend cookie eith refresh token to the http response -добавить файл cookie с токеном обновления в ответ http
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions); 
        }

        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
