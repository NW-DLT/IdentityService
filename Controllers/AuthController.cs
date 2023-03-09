using Microsoft.AspNetCore.Mvc;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Services;
using System.Net;


namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        [HttpGet(Name = "LoginUser")]
        public async Task<IActionResult> Login(string UserName, string UserPasswor)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                var message = string.Format("User with UserName = {0} not found", UserName);
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent(message);
                return (IActionResult)Task.FromResult(response);
            }
            var result = await _signInManager.PasswordSignInAsync(UserName, UserPasswor, false, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpPost(Name = "RegisterUser")]
        public async Task<IActionResult> Register(string UserName, string UserPassword)
        {
            var user = new AppUser { UserName = UserName };
            var result = await _userManager.CreateAsync(user, UserPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }
            return BadRequest();
        }
    }
}
