using AITestBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Controllers
{
    public class ErrorHandlerController : Controller
    {

        #region Singleton
        private static ErrorHandlerController instance = null;

        private ErrorHandlerController()
        {

        }

        public static ErrorHandlerController GetInstance()
        {
            if (instance == null)
            {
                instance = new ErrorHandlerController();
            }
            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }
        #endregion

        public IActionResult Error(string ErrorMessage)
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
