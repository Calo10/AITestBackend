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
    public class PatientHistory : BaseController
    {

        #region PatientHistory

        [HttpPost("[controller]/SaveHistory")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public Response SaveHistory([FromBody]PatientHistoryModel attachmentsModel)
        {
            Response ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = PatientHistoryModel.SaveAttachment(attachmentsModel);
                }
                else
                {
                    ans = new Response() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
                }
            }
            catch (Exception ex)
            {
                ans = new Response() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_GenericExceptionError };
            }

            return ans;
        }

        [HttpGet("[controller]/GetAllHistories")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ResponseHistoriesList GetAllHistories([FromBody]PatientHistoryModel identification)
        {

            ResponseHistoriesList ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = PatientHistoryModel.GetAllHistories(identification);
                }
                else
                {
                    ans = new ResponseHistoriesList() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
                }
            }
            catch (Exception ex)
            {
                ans = new ResponseHistoriesList() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_GenericExceptionError };
            }

            return ans;
        }

        //[HttpPost("[controller]/DeleteAttachment")]
        //[Produces("application/json")]
        //[ProducesResponseType(200)]
        //public Response DeleteAttachment([FromBody]AttachmentsModel attachmentsModel)
        //{
        //    Response ans = null;

        //    try
        //    {
        //        if (ValidateSecurityAPI())
        //        {
        //            ans = AttachmentsModel.DeleteAttachment(attachmentsModel);
        //        }
        //        else
        //        {
        //            ans = new Response() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ans = new Response() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_GenericExceptionError };
        //    }
        //    return ans;
        //}

        #endregion


    }
}