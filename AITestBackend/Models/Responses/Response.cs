using System;
using System.Collections.Generic;

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


    public class PatientTreatmentDeseaseList : Response
    {
        public List<PatientTreatmentDeseaseModel> PatientTreatmentDeseases { get; set; }
    }

}
