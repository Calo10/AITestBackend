using AITestBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AITestBackend.Controllers
{
    public class BaseController : Controller
    {

        public IActionResult Error(string ErrorMessage)
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Security API Validator
        /// </summary>
        /// <returns></returns>
        protected bool ValidateSecurityAPI()
        {
            var AccessToken = string.Empty;

            if (Request.Headers.TryGetValue("AccessToken", out Microsoft.Extensions.Primitives.StringValues headerValues))
            {
                AccessToken = headerValues.FirstOrDefault();
            }

            return SecurityController.GetInstance().ValidateSecurityToken(AccessToken);
        }
    }
}
