using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory();
factory.HostName = "localhost";
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: "logs", routingKey: string.Empty);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var byteMessage = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(byteMessage);
    channel.BasicAck(ea.DeliveryTag, multiple: false);
    Console.WriteLine("Okunan Mesaj : " + message);
};

channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

Console.ReadKey();