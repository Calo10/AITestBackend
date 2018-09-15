using System;
using System.Collections.Generic;

namespace AITestBackend.Models
{
    public class SendMessageToAssistantResponse : Response
    {
        public List<string> lstMessages { get; set; }
    }
}
