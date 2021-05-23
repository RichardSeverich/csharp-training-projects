namespace Medium.Publications.RabbitMQEventBus
{
    public record RabbitMqConnectionSettings(
        string Host, 
        string Port,
        string UserName, 
        string Password);
}
