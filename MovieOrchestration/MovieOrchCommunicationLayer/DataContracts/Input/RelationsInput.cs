using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieOrchCommunicationLayer.DataContracts.Input
{
    [DataContract]
    public class RelationsInput : BaseInput
    {
        [DataMember]
        public int ParentId { get; set; }
        [DataMember]
        public int ChildId { get; set; }
        [DataMember]
        public string CharacterName { get; set; }
    }
}