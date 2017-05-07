using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UserCommunicationLayer.DataContracts.Input
{
    [DataContract]
    public class ClientInput: BaseInput
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string ClientPassword { get; set; }
    }
}