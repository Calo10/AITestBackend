using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class ResponsePatients : Response
    {
        public List<PatientModel> Patients { get; set; }
    }
}
