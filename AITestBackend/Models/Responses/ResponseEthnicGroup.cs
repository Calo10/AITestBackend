using System.Collections.Generic;

namespace AITestBackend.Models
{
    public class ResponseEthnicGroup : Response
    {
        public List<EthnicModel> Ethnics { get; set; }
    }
}
