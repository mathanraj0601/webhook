using ClimateWebhookAgent.Data;
using ClimateWebhookAgent.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimateWebhookAgent.AppHost
{
    public class AppHost : IAppHost
    {
        private readonly ClimateContext _context;
        public AppHost(ClimateContext context)
        {
            _context = context;
        }
        public void Run()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "webhook", type: ExchangeType.Fanout);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: "webhook",
                              routingKey: "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var webHookWorkerDto = JsonSerializer.Deserialize<WebHookWorkerDto>(message);

                //var webhooks = _context.Subscibers.Where(x => x.WebHookType == webHookWorkerDto.WebHookType && x.Publisher == webHookWorkerDto.Publisher && x.Area == webHookWorkerDto.Area).ToList();
            };

          
        }
    }
}
