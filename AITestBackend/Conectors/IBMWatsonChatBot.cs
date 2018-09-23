using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AITestBackend.Models;
using IBM.WatsonDeveloperCloud.Assistant.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using IBM.WatsonDeveloperCloud.Assistant.v1.Model;
using Newtonsoft.Json;

namespace AITestBackend.Conectors
{
    public class IBMWatsonChatBot
    {

        public IBMWatsonChatBot()
        {
        }

        public static IBM.WatsonDeveloperCloud.Assistant.v1.Model.MessageResponse SendMessageToAssistant(string message, IBM.WatsonDeveloperCloud.Assistant.v1.Model.Context context)
        {

             var _assistant = new AssistantService(AppManagement.UserName, AppManagement.Password, AppManagement.VersionDate);

            MessageRequest messageRequest;

            if (context == null)
            {
                messageRequest = new MessageRequest()
                {
                    Input = new InputData()
                    {
                        Text = message
                    }
                };
            }
            else
            {
                messageRequest = new MessageRequest()
                {
                    Input = new InputData()
                    {
                        Text = message
                    },
                    Context = context
                };
            }
            

            var result = _assistant.Message(AppManagement.WorkspaceID, messageRequest);

            return result;
        }
    }
}