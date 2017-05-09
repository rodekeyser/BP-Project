using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieOrchCommunicationLayer.DataContracts.Input
{
    [DataContract]
    public class BaseInput
    {
        [DataMember]
        public string Application { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}