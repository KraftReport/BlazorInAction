using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blazortrailsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        [Route("get-data")]
        public IActionResult GetData()
        {
            return Ok("data");
        }
    }
}
