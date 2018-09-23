using System;
using System.Collections.Generic;

namespace AITestBackend.Models
{
    public class SendMessageToAssistantResponse : Response
    {
        public List<string> lstMessages { get; set; }

        public IBM.WatsonDeveloperCloud.Assistant.v1.Model.Context Context { get; set; }
    }
}
