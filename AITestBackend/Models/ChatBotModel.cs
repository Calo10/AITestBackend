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

        public static SendMessageToAssistantResponse SendMessageToAssistant(string messsage)
        {
            List<string> lstMessages = new List<string>();

            try
            {
                IBM.WatsonDeveloperCloud.Assistant.v1.Model.MessageResponse response = IBMWatsonChatBot.SendMessageToAssistant(messsage);

                var aasd = response.Output.text;

                foreach (var item in response.Output.text)
                {
                    lstMessages.Add(item.ToString());
                }
                //Pending validation error from API
                return new SendMessageToAssistantResponse { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SendMessageToAssistantResponse_Succesful, lstMessages = lstMessages };
            }
            catch (Exception ex)
            {
                return new SendMessageToAssistantResponse { IsSuccessful = false, ResponseMessage = ErroMessagesTranslate.TranslateException(ex), lstMessages = lstMessages };
            }
        }
    }
}
