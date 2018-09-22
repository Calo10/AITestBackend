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
        public string GetAll(string parentId)
        {
            ResponsePatients ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = PatientModel.GetAll(parentId);
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

        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public string SavePatient([FromBody]PatientModel patientModel)
        {
            Response ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    if (patientModel.IsNotNull())
                    {
                        ans = PatientModel.SavePatient(patientModel);
                    }
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

        [HttpGet()]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public string Get(string identification)
        {
            ResponsePatient ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = PatientModel.GetPatient(identification);
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