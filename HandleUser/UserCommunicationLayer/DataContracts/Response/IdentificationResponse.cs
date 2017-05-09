using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UserCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class IdentificationResponse : BaseResponse
    {
        [DataMember]
        public int Id { get; set; }
    }
}