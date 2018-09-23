using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AITestBackend.Models
{
    public class ResponseAttachments : Response
    {
        public List<AttachmentsModel> Attachments { get; set; }
    }
}
