using AITestBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace AITestBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/patient")]
    public class PatientController : BaseController
    {
        [HttpGet("list")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public string Get()
        {
            ResponsePatients ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = PatientModel.GetAll();
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
    }
}