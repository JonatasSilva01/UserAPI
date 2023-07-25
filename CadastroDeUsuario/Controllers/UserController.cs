using CadastroDeUsuario.Data.Dtos;
using CadastroDeUsuario.Services;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeUsuario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // private UserContext _userContext;
        private UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            await _userServices.RegisterAsync(dto);
            return Ok("User created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto dto) 
        {
            var token = await _userServices.LoginAsync(dto);
            return Ok(token);
        }
    }
}
