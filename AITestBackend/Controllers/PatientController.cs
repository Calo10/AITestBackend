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
        public ResponsePatients GetAll(string parentId)
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
                    ans = new ResponsePatients() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
                }
            }
            catch (Exception ex)
            {
                ans = new ResponsePatients() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_GenericExceptionError };
            }

            return ans;
        }

        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public Response SavePatient([FromBody]PatientModel patientModel)
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
                    ans = new ResponsePatient() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
                }
            }
            catch (Exception ex)
            {
                ans = new ResponsePatient() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_SaveTreatment_Duplicate };
            }

            return ans;
        }

        [HttpGet()]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ResponsePatient Get(string identification)
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
                    ans = new ResponsePatient() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
                }
            }
            catch (Exception ex)
            {
                ans = new ResponsePatient() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_GenericExceptionError };
            }

            return ans;
        }
    }
}