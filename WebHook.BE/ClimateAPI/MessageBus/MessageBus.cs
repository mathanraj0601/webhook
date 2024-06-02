using ClimateAPI.Model.DTO;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ClimateAPI.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        public MessageBus()
        {
            _connectionFactory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            _connection = _connectionFactory.CreateConnection();
        }
        public void publish(WebHookWorkerDto webHookWorkerDto)
        {
            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: "webhook", type: ExchangeType.Fanout);

            var content = JsonSerializer.Serialize(webHookWorkerDto);
            var body = Encoding.UTF8.GetBytes(content);

            channel.BasicPublish("webhook",
                            routingKey: string.Empty,
                            basicProperties: null,
                            body: body);


        }
    }
}
