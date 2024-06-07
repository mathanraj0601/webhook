using AutoMapper;
using ClimateAPI.Data;
using ClimateAPI.Model;
using ClimateAPI.Model.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClimateAPI.Controllers
{
    [EnableCors("MyCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly IRepo<Subscriber, int> _repo;
        private readonly IMapper _mapper;
        public SubscriberController(IRepo<Subscriber, int> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SubscriberResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubscriberResponseDto>> GetAll()
        {
            var subscribersQueryable = await _repo.GetAll();
            var subscribers = await subscribersQueryable.ToListAsync();
            var results = _mapper.Map<List<Subscriber>>(subscribers);
            return Ok(results);
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(SubscriberResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubscriberResponseDto>> Get([FromQuery] int id)
        {
            var subscriber = await _repo.Get(id);
            if (subscriber == null)
                return NotFound();
            var result = _mapper.Map<Subscriber>(subscriber);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SubscriberResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriberResponseDto>> Add([FromBody] SubscriberCreateDto subscriberCreateDto)
        {
            subscriberCreateDto.Secret = Guid.NewGuid();
            var subscriber = _mapper.Map<Subscriber>(subscriberCreateDto);
            var result = await _repo.Add(subscriber);
            var subscriberReponse = _mapper.Map<Subscriber>(result);
            return CreatedAtAction(nameof(Get), new { id = subscriberReponse.Id }, subscriberReponse);
        }

        [HttpPut]
        [ProducesResponseType(typeof(SubscriberResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriberResponseDto>> Update([FromBody] SubscriberUpdateDto subscriberUpdateDto)
        {
            var subscriber = _mapper.Map<Subscriber>(subscriberUpdateDto);
            var result = await _repo.Update(subscriber);
            var subscriberReponse = _mapper.Map<Subscriber>(result);
            return Ok(subscriberReponse);
        }

        [HttpDelete("id")]
        [ProducesResponseType(typeof(SubscriberResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriberResponseDto>> Delete([FromQuery] int id)
        {
            var result = await _repo.Delete(id);
            var subscriberReponse = _mapper.Map<Subscriber>(result);
            return Ok(subscriberReponse);
        }

    }
}
