using AgentAPI.Model;
using AutoMapper;

namespace AgentAPI.Mapper
{
    public class WebHookMapper : Profile
    {
        public WebHookMapper()
        {
            CreateMap<WebHookCreateDto, WebHook>();
            CreateMap<WebhookUpdateDto, WebHook>();
            CreateMap<WebHook, WebHookResponseDto>();
        }
    }
}
