using AutoMapper;
using ClimateAPI.Model;
using ClimateAPI.Model.DTO;

namespace ClimateAPI.Mapper
{
    public class ClimateMapper :Profile
    {
        public ClimateMapper() 
        {
            CreateMap<ClimateCreateDto, Climate>();
            CreateMap<ClimateUpdateDto, Climate>();
            CreateMap<Climate, ClimateResponseDto>();
        }
    }
}
