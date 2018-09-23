using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AITestBackend.Models;
using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;

using Newtonsoft.Json;

namespace AITestBackend.Conectors
{
    public class IBMWatsonSpeechToText
    {

        public IBMWatsonSpeechToText()
        {
        }

        public async static void SendMessageToAssistant(byte[] filePath)
        {

            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri("https://stream.watsonplatform.net/speech-to-text/api/v1/recognize");
                MultipartFormDataContent form = new MultipartFormDataContent();

                client.DefaultRequestHeaders.Add("Postman-Token", "5a600998-d529-4d9e-9be0-1afcccde53be");
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.Add("Authorization", "Basic N2NlNTViMTUtNDllMy00ZjZkLWJhODAtNGIwZDI2OTIwY2UzOnBoRFR5VjNWZUxtZA==");
                //client.DefaultRequestHeaders.Add("Content-Type", "audio/flac");
                


                var json = JsonConvert.SerializeObject(new
                {
                    CultureInfo = filePath
                });
                form.Add(new StringContent(json, Encoding.UTF8, "audio/flac"), "password");

                form.Add(new StreamContent(new MemoryStream(filePath)), "bilddatei");

                HttpResponseMessage response = await client.PostAsync(uri, form);

                response.EnsureSuccessStatusCode();
                string ans = response.Content.ReadAsStringAsync().Result;
                

            }


        }


    }
}