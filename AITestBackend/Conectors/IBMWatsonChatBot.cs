using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AITestBackend.Models;
using IBM.WatsonDeveloperCloud.Assistant.v1;
using IBM.WatsonDeveloperCloud.Assistant.v1.Model;
using Newtonsoft.Json;

namespace AITestBackend.Conectors
{
    public class IBMWatsonChatBot
    {

        public IBMWatsonChatBot()
        {
        }

        public static IBM.WatsonDeveloperCloud.Assistant.v1.Model.MessageResponse SendMessageToAssistant(string message)
        {

             var _assistant = new AssistantService(AppManagement.UserName, AppManagement.Password, AppManagement.VersionDate);

            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = message
                }
            };

            var result = _assistant.Message(AppManagement.WorkspaceID, messageRequest);

            return result;
        }
    }
}