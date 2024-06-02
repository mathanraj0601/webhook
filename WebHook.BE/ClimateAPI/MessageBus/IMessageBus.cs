using ClimateAPI.Model.DTO;

namespace ClimateAPI.MessageBus
{
    public interface IMessageBus
    {
        public void publish(WebHookWorkerDto webHookWorkerDto);
    }
}
