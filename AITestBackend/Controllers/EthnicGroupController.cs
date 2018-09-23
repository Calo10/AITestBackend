using AITestBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace AITestBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/ethnicgroup")]
    public class EthnicGroupController : BaseController
    {
        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ResponseEthnicGroup Get()
        {
            ResponseEthnicGroup ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = EthnicModel.GetAll();
                }
                else
                {
                    ans = new ResponseEthnicGroup() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
                }
            }
            catch (Exception ex)
            {
                ans = new ResponseEthnicGroup() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_GenericExceptionError };
            }

            return ans;
        }
    }
}
