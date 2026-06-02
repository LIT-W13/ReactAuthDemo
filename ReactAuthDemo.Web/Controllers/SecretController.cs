using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReactAuthDemo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        [Authorize]
        [HttpGet("getdata")]
        public object GetData()
        {
            var rnd = new Random();
            return new
            {
                number = rnd.Next(1, 1000)
            };
        }
    }
}
