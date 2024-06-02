using AutoMapper;
using ClimateAPI.Model;
using ClimateAPI.Model.DTO;

namespace ClimateAPI.Mapper
{
    public class SubscribeMapper : Profile
    {
        public SubscribeMapper()
        {
            CreateMap<SubscriberCreateDto, Subscriber>();
            CreateMap<SubscriberUpdateDto, Subscriber>();
            CreateMap<Subscriber,SubscriberResponseDto>();
        }
    }
}
