using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReactAuthDemo.Data;
using ReactAuthDemo.Web.Models;
using System.Security.Claims;

namespace ReactAuthDemo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly string _connectionString;

        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("Signup")]
        public void Signup(SignupViewModel viewModel)
        {
            var repo = new UserRepoitory(_connectionString);
            repo.Signup(viewModel, viewModel.Password);
        }

        [HttpPost("login")]
        public User Login(LoginViewModel viewModel)
        {
            var repo = new UserRepoitory(_connectionString);
            var user = repo.Login(viewModel.Email, viewModel.Password);

            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, viewModel.Email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", ClaimTypes.Email, "role"))).Wait();

            return user;
        }

        [HttpGet("getcurrentuser")]
        public User GetCurrentUser()
        {
            Thread.Sleep(2000);
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }
            var repo = new UserRepoitory(_connectionString);
            var email = User.Identity.Name;
            return repo.GetByEmail(email);
        }

        [HttpPost("logout")]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }


        
        
    }
}
