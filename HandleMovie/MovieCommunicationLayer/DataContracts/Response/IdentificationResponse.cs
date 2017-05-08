using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class IdentificationResponse: BaseResponse
    {
        [DataMember]
        public int Id { get; set; }
    }
}