using System;
namespace AITestBackend.Models
{
    public class Response
    {
        public bool IsSuccessful { get; set; }
        public string ResponseMessage { get; set; }

    }


    public class ResponseParent : Response
    {
        public ParentModel Parent { get; set; }
    }

}
