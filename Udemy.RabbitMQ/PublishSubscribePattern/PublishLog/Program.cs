using System.Text;
using RabbitMQ.Client;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};
var connection = connectionFactory.CreateConnection();
var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

var message = GetMessage(args);

var byteMessage = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "logs", routingKey: string.Empty, basicProperties: null, body: byteMessage);


Console.WriteLine("Mesaj gönderildi : " + message);

static string GetMessage(string[] args)
{
    return args.Length > 0 ? string.Join(" ", args) : "info : Hello world!!!";
}

Console.ReadLine();