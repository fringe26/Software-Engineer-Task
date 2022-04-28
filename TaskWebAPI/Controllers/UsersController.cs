using Microsoft.AspNetCore.Mvc;
using TaskWebAPI.Data;
using TaskWebAPI.DTO.Users;
using TaskWebAPI.Models;

namespace TaskWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private readonly IAuthRepository _authRepo;
        public UsersController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
            ServiceResponse<int> response = await _authRepo.Register(
                new User { Name = user.Name}, user.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            ServiceResponse<string> response = await _authRepo.Login(
                user.Name, user.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
