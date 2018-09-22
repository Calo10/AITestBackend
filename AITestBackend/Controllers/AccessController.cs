using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AITestBackend.Models;
using Newtonsoft.Json;

namespace AITestBackend.Controllers
{
    public class AccessController : BaseController
    {

        #region Parent

        /// <summary>
        /// Get a parent 
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     GET Access/GetParent
        ///     {
        ///         identification: 1
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Returns a parent</response>
        [HttpGet("[controller]/GetParent")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public string GetParent(string identification)
        {

            ResponseParent ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = ParentModel.GetParent(identification);
                }
                else
                {
                    return AppManagement.MSG_API_Validation_Failure;
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }

            return JsonConvert.SerializeObject(ans);
        }




        #endregion
    }
}