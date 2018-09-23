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
    public class AttachmentsController : BaseController
    {

        #region Attachment
                       
        [HttpPost("[controller]/SaveAttachment")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public Response SaveAttachment([FromBody]AttachmentsModel attachmentsModel)
        {
            Response ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = AttachmentsModel.SaveAttachment(attachmentsModel);
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

        [HttpGet("[controller]/GetParent")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ResponseAttachmentsList GetAllAttachments(string identification)
        {

            ResponseAttachmentsList ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = AttachmentsModel.GetAllAttachments(identification);
                }
                else
                {
                    ans = new ResponseAttachmentsList() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
                }
            }
            catch (Exception ex)
            {
                ans = new ResponseAttachmentsList() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_GenericExceptionError };
            }

            return ans;
        }

        #endregion


    }
}