namespace AuthService.RabbitMQ
{
    public interface IRabbitMQPublisher
    {
        void PublishMessage(object message, string topicName);
    }
}
