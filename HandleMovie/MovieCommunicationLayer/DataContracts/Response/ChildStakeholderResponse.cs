using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class ChildStakeholderResponse
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FamilyName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
    }
}