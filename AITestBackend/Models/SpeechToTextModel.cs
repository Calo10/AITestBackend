using System;
using System.Collections.Generic;
using AITestBackend.Conectors;
using AITestBackend.Helpers;

namespace AITestBackend.Models
{
    public class SpeechToTextModel
    {
        public SpeechToTextModel()
        {
        }

        public static void SendMessageToAssistant(byte[] messsage)
        {
            List<string> lstMessages = new List<string>();

            try
            {
                byte[] newbyte = new byte[1];
                //IBM.WatsonDeveloperCloud.Assistant.v1.Model.MessageResponse response = 
                IBMWatsonSpeechToText.SendMessageToAssistant(messsage);

                //var aasd = response.Output.text;

                //foreach (var item in response.Output.text)
                //{
                //    lstMessages.Add(item.ToString());
                //}
                //Pending validation error from API
                //return new SendMessageToAssistantResponse { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SendMessageToAssistantResponse_Succesful, lstMessages = lstMessages };
            }
            catch (Exception ex)
            {
                //return new SendMessageToAssistantResponse { IsSuccessful = false, ResponseMessage = ErroMessagesTranslate.TranslateException(ex), lstMessages = lstMessages };
            }
        }
    }
}
