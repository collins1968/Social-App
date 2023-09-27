using EmailService.Models;
using EmailService.Service;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailService.Messaging
{
    public class RaabbitMqRegister : BackgroundService
    {
        private readonly IConnection _connection;
        private IModel _channel;
        private IConfiguration _configuration;
        private readonly Emailservice _saveToDb;
        private readonly EmailSendService _emailService;
        public RaabbitMqRegister(Emailservice service, IConfiguration configuration)
        {
            _configuration = configuration;
            _saveToDb = service;
            _emailService = new EmailSendService(_configuration);

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("postregisteruser", false, false, false, null); 
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, arg) =>
            {
                var content = Encoding.UTF8.GetString(arg.Body.ToArray());
                var userMessage = JsonConvert.DeserializeObject<UserMessage>(content);
                //send email & save to dB
                sendEmail(userMessage).GetAwaiter().GetResult();


                //its Done Now delete from Queue
                _channel.BasicAck(arg.DeliveryTag, false);
            };

            _channel.BasicConsume("postregisteruser", false, consumer);

            return Task.CompletedTask;
        }
        private async Task sendEmail(UserMessage userMessage)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2023/04/20/10/19/coding-7939372_1280.jpg\" width=\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + userMessage.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to The Jitu Shopping Site ");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p> Start Shopping here</p>");
                var emailLogger = new EmailLoggers()
                {
                    Email = userMessage.Email,
                    Message = stringBuilder.ToString()

                };
                await _saveToDb.SaveData(emailLogger);
                await _emailService.SendEmail(userMessage, stringBuilder.ToString());


            }
            catch (Exception ex) { }
        }
    }
}
