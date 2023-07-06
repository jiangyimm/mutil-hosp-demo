using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class Publisher
{
    public async Task StartBasicPublish()
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672/")
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "hello",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        var i = 0;
        while (true)
        {
            string message = "Hello World ";
            message = message + i++.ToString();
            var body = System.Text.Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                                       routingKey: "hello",
                                       basicProperties: properties,
                                       body: body);
            await Task.Delay(500);
        }
        //Console.WriteLine("sent {0}", message);
    }
}
public class Consumer
{
    public async Task StartBasicConsumer1()
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672/")
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine("consumer1 => {0}", message);
            await Task.Delay(5000);
        };

        channel.BasicConsume(queue: "hello",
                            autoAck: true,
                            consumer: consumer);
    }
    public async Task StartBasicConsumer2()
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672/")
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine("consumer2 => {0}", message);
            await Task.Delay(5000);
        };

        channel.BasicConsume(queue: "hello",
                            autoAck: true,
                            consumer: consumer);
    }

    public async Task StartBasicConsumer3()
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672/")
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine("consumer3 => {0}", message);
            await Task.Delay(5000);
        };

        channel.BasicConsume(queue: "hello",
                            autoAck: true,
                            consumer: consumer);
    }
}