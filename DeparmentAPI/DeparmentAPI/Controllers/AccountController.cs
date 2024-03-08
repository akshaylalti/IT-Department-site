using DeparmentAPI.Model;
using DeparmentAPI.Ripository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OpenAI_API.Completions;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DeparmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IApplictionRepository _applictionRepository;
        private readonly UserContext _userContext;

        public AccountController(IApplictionRepository applictionRepository, UserContext userContext)
        {
            _applictionRepository = applictionRepository;
            _userContext = userContext;
        }


        [HttpPost("signUp")]

        public async Task<IActionResult> SignUp([FromBody] SignInModel model)
        {
            try
            {
                var result = await _applictionRepository.SignUpAsync(model);
                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }
                //if (string.IsNullOrEmpty(result))
                //{
                //    return Ok(result);
                //}
            }
            catch (Exception ex)
            {
                return null;

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
            return Ok(await _userContext.ApplictionUsers.ToListAsync());
        }

        [Authorize]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetOneUser([FromRoute] string id)
        {
            var result = await _applictionRepository.GetUserbyIdAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("allStudent")]
        public async Task<ActionResult<List<ApplictionUser>>> getAllStudent()
        {
            var std = await _userContext.ApplictionUsers.ToListAsync();
            return Ok(std);
        }

        [HttpGet("result/{id}")]
        public async Task<IActionResult> getResult([FromRoute] string id)
        {
            var result = await _applictionRepository.GetUserbyIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] ApplictionUser applictionUser)
        {
            var result = await _applictionRepository.UpdateUserDetails(applictionUser);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        //open ai
       

    }
}
