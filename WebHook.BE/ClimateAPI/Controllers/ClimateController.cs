using AutoMapper;
using ClimateAPI.Data;
using ClimateAPI.MessageBus;
using ClimateAPI.Model;
using ClimateAPI.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClimateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimateController : ControllerBase
    {
        private readonly IRepo<Climate, int> _repo;
        private readonly IMapper _mapper;
        public ClimateController(IRepo<Climate,int> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ClimateResponseDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClimateResponseDto>> GetAll()
        {
            var climatesQueryable = await _repo.GetAll();
            var climates = await climatesQueryable.ToListAsync();
            var results = _mapper.Map<List<Climate>>(climates);
            return Ok(results);
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(ClimateResponseDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClimateResponseDto>> Get([FromQuery] int id)
        {
            var climate = await _repo.Get(id);
            if (climate == null)
                return NotFound();
            var result = _mapper.Map<Climate>(climate);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClimateResponseDto),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClimateResponseDto>> Add([FromBody] ClimateCreateDto climateCreateDto)
        {
            var climate = _mapper.Map<Climate>(climateCreateDto);
            var result = await _repo.Add(climate);
            var climateReponse = _mapper.Map<Climate>(result);
            return CreatedAtAction(nameof(Get), new { id = climateReponse.Id }, climateReponse);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ClimateResponseDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClimateResponseDto>> Update([FromBody] ClimateUpdateDto climateUpdateDto)
        {
            var climate = _mapper.Map<Climate>(climateUpdateDto);
            var result = await _repo.Update(climate);
           
            var climateReponse = _mapper.Map<Climate>(result);
            return Ok(climateReponse);
        }

        [HttpDelete("id")]
        [ProducesResponseType(typeof(ClimateResponseDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClimateResponseDto>> Delete([FromQuery]int id)
        {
            var result = await _repo.Delete(id);
            var climateReponse = _mapper.Map<Climate>(result);
            return Ok(climateReponse);
        }


    }
}
