using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traceability.Example.MessageBrokers.Shared.Messages
{
    public interface IMessage
    {
        string Text { get; set; }
    }
}
