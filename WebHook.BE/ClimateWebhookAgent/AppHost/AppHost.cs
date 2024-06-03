using ClimateWebhookAgent.Data;
using ClimateWebhookAgent.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ClimateWebhookAgent.AppHost
{
    public class AppHost : IAppHost
    {
        private readonly ClimateContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        public AppHost(ClimateContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            ThreadPool.SetMaxThreads(8,8);
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
                if (webHookWorkerDto == null)
                    return;
                if (_context.Subscribers == null)
                    return ;
                var subscribers = _context.Subscribers
                                       .Where(x => x.WebHookType == webHookWorkerDto.WebHookType && x.Publisher == webHookWorkerDto.Publisher ).ToList();
                foreach (var subscriber in subscribers)
                {
                    var priceChangeDto = new PriceChangeDto
                    {
                        OldTemp = webHookWorkerDto.OldTemp,
                        NewTemp = webHookWorkerDto.NewTemp,
                        Area = webHookWorkerDto.Area,
                        WebhookType = webHookWorkerDto.WebHookType,
                        WebhookUrl = subscriber.SubscriberUrl,
                        Secret = subscriber.Secret.ToString(),
                        Publisher = subscriber.Publisher
                    };
                    ThreadPool.QueueUserWorkItem(async (state) => await SendData(priceChangeDto));
                }
            };

            channel.BasicConsume(queue: queueName,
                                   autoAck: true,
                                  consumer: consumer);

            Console.ReadLine();

          
        }


        public async Task SendData(PriceChangeDto priceChangeDto)
        {
            using var client = _httpClientFactory.CreateClient();
            var webhookMessageBytes = JsonSerializer.SerializeToUtf8Bytes(priceChangeDto);
            var webhookMessage = new HttpRequestMessage(HttpMethod.Post, priceChangeDto.WebhookUrl)
            {
                Content = new ByteArrayContent(webhookMessageBytes)
            };
            webhookMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            await client.SendAsync(webhookMessage);
        }
    }
}
