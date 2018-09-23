using System;
using System.Collections.Generic;
using AITestBackend.Conectors;
using AITestBackend.Helpers;

namespace AITestBackend.Models
{
    public class ChatBotModel
    {
        public ChatBotModel()
        {
        }

        public static SendMessageToAssistantResponse SendMessageToAssistant(string messsage, IBM.WatsonDeveloperCloud.Assistant.v1.Model.Context context)
        {
            List<string> lstMessages = new List<string>();

            try
            {
                IBM.WatsonDeveloperCloud.Assistant.v1.Model.MessageResponse response = IBMWatsonChatBot.SendMessageToAssistant(messsage, context);


                var aasd = response.Context;


                foreach (var item in response.Output.text)
                {
                    lstMessages.Add(item.ToString());
                }
                //Pending validation error from API
                return new SendMessageToAssistantResponse { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SendMessageToAssistantResponse_Succesful, lstMessages = lstMessages,Context = response.Context };
            }
            catch (Exception ex)
            {
                return new SendMessageToAssistantResponse { IsSuccessful = false, ResponseMessage = ErroMessagesTranslate.TranslateException(ex), lstMessages = lstMessages };
            }
        }
    }
}
