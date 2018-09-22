using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AITestBackend.Models;

namespace AITestBackend.Controllers
{
    public class AccessController : Controller
    {

        public IActionResult Index()
        {
            return View();
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

            return SecurityController.GetInstance().ValidateSecurityToken(AccessToken);
        }
    }
}