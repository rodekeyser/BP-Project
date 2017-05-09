using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieOrchCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class ClientListResponse: BaseResponse
    {
        [DataMember]
        public List<ClientResponse> Clients { get; set; }
        public ClientListResponse()
        {
            this.Clients = new List<ClientResponse>();
        }
    }
}