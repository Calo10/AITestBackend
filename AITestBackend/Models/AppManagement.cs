using System;
namespace AITestBackend.Models
{
    public class AppManagement
    {

        //APP 
        public const string APIS_Token = "";

        //IBM Chatbot
        public const string WorkspaceID = "799aecc9-dcb3-41db-b85a-36043cf448b9";
        public const string WorkspaceURL = "https://gateway.watsonplatform.net/assistant/api/v1/workspaces/" + WorkspaceID + "/message/";

        public const string UserName = "93ac3419-e3a6-4565-a6ee-6a086ba7d1a8";
        public const string Password = "nYnCFtwdjuAs";
        public const string VersionDate = "2018-07-10";

        //Messages
        public const string MSG_API_Validation_Failure = "Se ha producido un error";
        public const string MSG_SendMessageToAssistantResponse_Succesful = "";

        public const string MSG_GenericExceptionError = "Se ha producido un error en el Servidor";
    }
}
