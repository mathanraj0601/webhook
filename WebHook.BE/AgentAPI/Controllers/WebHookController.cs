using AgentAPI.Data;
using AgentAPI.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHookController : ControllerBase
    {
        private readonly IRepo<WebHook, int> _repo;
        private readonly IMapper _mapper;
        public WebHookController(IRepo<WebHook,int> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WebHookCreateDto webHookCreateDto)
        {
            if(webHookCreateDto == null)
            {
                return BadRequest();
            }
            var webHook = _mapper.Map<WebHook>(webHookCreateDto);
            var result = await _repo.Add(webHook);
            var webHookResponseDto = _mapper.Map<WebHookResponseDto>(result);
            return Ok(webHookResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] WebhookUpdateDto webHookUpdateDto)
        {
            if(webHookUpdateDto == null)
            {
                return BadRequest();
            }
            var webHook = _mapper.Map<WebHook>(webHookUpdateDto);
            webHook.Id = id;
            var result = await _repo.Update(webHook);
            var webHookResponseDto = _mapper.Map<WebHookResponseDto>(result);
            return Ok(webHookResponseDto);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetAll();
            var webHookResponseDto = _mapper.Map<IEnumerable<WebHookResponseDto>>(result);
            return Ok(webHookResponseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repo.Get(id);
            var webHookResponseDto = _mapper.Map<WebHookResponseDto>(result);
            return Ok(webHookResponseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.Delete(id);
            var webHookResponseDto = _mapper.Map<WebHookResponseDto>(result);
            return Ok(webHookResponseDto);
        }


    }

}
