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
        public ResponseParent GetParent(string identification)
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
                    ans = new ResponseParent() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_API_Validation_Failure };
                }
            }
            catch (Exception ex)
            {
                ans = new ResponseParent() { IsSuccessful = false, ResponseMessage = AppManagement.MSG_GenericExceptionError };
            }

            return ans;
        }

        /// <summary>
        /// Save a PriceList for a Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST Access/RegisterParent
        ///     {   
        ///           "Identification": 304360398,
        ///           "Name": 'Lista de Descuento',
        ///           "Password": '123',
        ///           "Mobile": '89115991',
        ///           "Email": 'stevenariasfigueroa@gmail.com'
        ///     }
        /// </remarks>
        /// <param name="pPriceList"></param>
        /// <returns></returns>
        [HttpPost("[controller]/RegisterParent")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public Response RegisterParent([FromBody]ParentModel parentModel)
        {
            Response ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ans = ParentModel.RegisterParent(parentModel);
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

        #endregion

        #region login

        /// <summary>
        /// Get a parent 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET Access/Login
        ///     {
        ///         identification: 1,
        ///         password: 5
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Returns a parent</response>
        [HttpPost("[controller]/Login")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public Response Login(string Parent)
        {

            Response ans = null;

            try
            {
                if (ValidateSecurityAPI())
                {
                    ParentModel parent = JsonConvert.DeserializeObject<ParentModel>(Parent);
                    ans = ParentModel.Login(parent);
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

        #endregion
    }
}