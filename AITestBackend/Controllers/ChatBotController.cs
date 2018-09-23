using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AITestBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AITestBackend.Controllers
{
    public class ChatBotController : Controller
    {

        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public SendMessageToAssistantResponse SendMessageToBot(string message, [FromBody]IBM.WatsonDeveloperCloud.Assistant.v1.Model.Context context)
        {
            SendMessageToAssistantResponse ans = null;
            try
            {
                if (ValidateSecurityAPI())
                {

                    ans = ChatBotModel.SendMessageToAssistant(message);
                }
                else
                {
                    ans = new SendMessageToAssistantResponse() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }

            return ans;

        }

        public IActionResult Error(string ErrorMessage)
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        /// <summary>
        /// Security API Validator
        /// </summary>
        /// <returns></returns>
        private bool ValidateSecurityAPI()
        {
            var AccessToken = string.Empty;

            if (Request.Headers.TryGetValue("AccessToken", out Microsoft.Extensions.Primitives.StringValues headerValues))
            {
                AccessToken = headerValues.FirstOrDefault();
            }

            if (AccessToken == AppManagement.APIS_Token)
            {
                return true;
            }
            //Pending Access Logic
            return false;
        }
    }
}
