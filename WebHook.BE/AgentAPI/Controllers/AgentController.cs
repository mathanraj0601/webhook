using AgentAPI.Data;
using AgentAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IRepo<WebHook, int> _repo;
        public AgentController(IRepo<WebHook ,int>  repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            return Task.FromResult<IActionResult>(Ok("Hello World"));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClimateWebHookDto climateWebHookDto)
        {
            var webHooks =  await _repo.GetAll();
            var webHook = await webHooks.FirstOrDefaultAsync(x => x.Secret == climateWebHookDto.Secret && x.Publisher == climateWebHookDto.Publisher && x.WebHookType == climateWebHookDto.WebHookType);
            if(webHook == null)
            {
                Console.WriteLine("Invalid Webhook");
                return BadRequest();
            }
            else
            {
                Console.WriteLine($"Area : {climateWebHookDto.Area} ,  New Price : {climateWebHookDto.NewPrice}");
                Console.WriteLine("Valid Webhook");
                return Ok();
            }


        }

    }
}
