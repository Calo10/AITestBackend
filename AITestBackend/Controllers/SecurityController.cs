using AITestBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Controllers
{
    public class SecurityController : Controller
    {

        #region Singleton
        private static SecurityController instance = null;

        private SecurityController()
        {

        }

        public static SecurityController GetInstance()
        {
            if (instance == null)
            {
                instance = new SecurityController();
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

        #region Security APIs

        public string SignUp()
        {
            Response ans = null;
            try
            {

            }
            catch (Exception ex)
            {

                ErrorHandlerController.GetInstance().Error(ex.Message);
            }

            return JsonConvert.SerializeObject(ans);

        }
        #endregion

        /// <summary>
        /// Security API Validator
        /// </summary>
        /// <returns></returns>
        public bool ValidateSecurityToken(string AccessToken)
        {
            if (AccessToken == AppManagement.APIS_Token)
            {
                return true;
            }
            //Pending Access Logic
            return false;
        }

    }
}
