using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSimpleSample.Models
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public DateTime Sent { get; set; }
    }
}
