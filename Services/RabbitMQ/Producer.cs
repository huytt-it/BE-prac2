using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Services.RabbitMQ
{

        public class Producer : BackgroundService
        {
            private readonly IConnection connection;
            private readonly IModel channel;
            private readonly IServiceScopeFactory _scopeFactory;
            private readonly IConfiguration _configuration;
            private EventingBasicConsumer consumer;
            public Producer(IServiceScopeFactory scopeFactory, IConfiguration configuration)
            {
                _scopeFactory = scopeFactory;
                _configuration = configuration;
                var factory = new ConnectionFactory();
                factory.ClientProvidedName = "Booking Subcriber 2";
                _configuration.Bind("RabbitMqConnection", factory);
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                InitRabbitMQ();
            }

            private void InitRabbitMQ()
            {
                var exchange = "logs";
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: exchange,
                                  routingKey: "");
                consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: queueName,
                  autoAck: true, consumer: consumer);
            }

            protected override Task ExecuteAsync(CancellationToken stoppingToken)
            {
                stoppingToken.ThrowIfCancellationRequested();
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var props = ea.BasicProperties;
                    try
                    {
                        var message = Encoding.UTF8.GetString(body);
                        // xử lý message
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                };
                return Task.CompletedTask;
            }
        }
    
}
