
using MassTransit;
using NLog;
using Traceability.Example.MessageBrokers.Shared.Messages;

string rabbitMQUri = "amqps://befjdvjy:dBfEg0GaJyIWuF1Yxd8J0z9CcOsErzk6@moose.rmq.cloudamqp.com/befjdvjy";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

var correlationId = Guid.NewGuid();

Logger _logger = LogManager.GetCurrentClassLogger();
System.Diagnostics.Trace.CorrelationManager.ActivityId = correlationId;
_logger.Debug("Publisher log");

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.Write("Gönderilecek mesaj : ");
string message = Console.ReadLine();
await sendEndpoint.Send<IMessage>(new ExampleMessage()
{
    Text = message
}, context =>
{
    context.Headers.Set("CorrelationId", correlationId);
});


Console.Read();