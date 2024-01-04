using DeparmentAPI.Model;
using DeparmentAPI.Ripository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DeparmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IApplictionRepository _applictionRepository;
        private readonly UserContext _userContext;

        public AccountController(IApplictionRepository applictionRepository,UserContext userContext)
        {
            _applictionRepository = applictionRepository;
            _userContext = userContext;
        }


        [HttpPost("signUp")]

        public async Task<IActionResult> SignUp([FromBody] SignInModel model)
        {
            var result = await _applictionRepository.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }

        [HttpPost("login")]

        public async Task<IActionResult> SignIn([FromBody] LoginModel loginModel)
        {
            var result = await _applictionRepository.LogInAsync(loginModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApplictionUser>> GetAllUsers()
        {
            return Ok(await _userContext.SignIn.ToListAsync());
        }

        [Authorize]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetOneUser([FromRoute] string id)
        {
            var result = await _applictionRepository.GetUserbyIdAsync(id);
            return Ok(result);
        }
    }
}
