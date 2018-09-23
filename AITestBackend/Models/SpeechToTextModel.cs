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

        public static SendMessageToSpeechToTextResponse SendMessageToAssistant(byte[] audio)
        {
            List<string> lstMessages = new List<string>();

            try
            {
                var response = IBMWatsonSpeechToText.SendMessageToAssistant(audio);

                foreach (var item in response.Results)
                {
                    lstMessages.Add(item.ToString());
                }
                
                return new SendMessageToSpeechToTextResponse { IsSuccessful = true, ResponseMessage = AppManagement.MSG_SendMessageToAssistantResponse_Succesful, lstMessages = lstMessages };
            }
            catch (Exception ex)
            {
                return new SendMessageToSpeechToTextResponse { IsSuccessful = false, ResponseMessage = ErroMessagesTranslate.TranslateException(ex), lstMessages = lstMessages };
            }
        }
    }
}
