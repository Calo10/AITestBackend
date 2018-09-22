using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class ResponsePatient : Response
    {
        public PatientModel Patient { get; set; }
    }
}
