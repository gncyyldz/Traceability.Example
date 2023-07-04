
using MassTransit;
using Traceability.Example.MessageBrokers.Consumer.Consumers;

string rabbitMQUri = "amqps://befjdvjy:dBfEg0GaJyIWuF1Yxd8J0z9CcOsErzk6@moose.rmq.cloudamqp.com/befjdvjy";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();