using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AITestBackend.Models;
using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;

namespace AITestBackend.Conectors
{
    public class IBMWatsonSpeechToText
    {

        public IBMWatsonSpeechToText()
        {
        }

        public static IBM.WatsonDeveloperCloud.SpeechToText.v1.Model.SpeechRecognitionResults SendMessageToAssistant(byte[] audio)
        {
            var _assistant = new SpeechToTextService(AppManagement.UserNameST, AppManagement.PasswordST);

            Stream stream = new MemoryStream(audio);

            var result = _assistant.Recognize("audio/flac", stream);//(AppManagement.WorkspaceIDST, messageRequest);

            return result;

        }


    }
}