using MassTransit;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traceability.Example.MessageBrokers.Shared.Messages;

namespace Traceability.Example.MessageBrokers.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen mesaj : {context.Message.Text}");

            var correlationId = Guid.NewGuid();
            if (context.Headers.TryGetHeader("CorrelationId", out object _correlationId))
                correlationId = Guid.Parse(_correlationId.ToString());

            Logger _logger = LogManager.GetCurrentClassLogger();
            System.Diagnostics.Trace.CorrelationManager.ActivityId = correlationId;
            _logger.Debug("Consumer log");

            return Task.CompletedTask;
        }
    }
}
