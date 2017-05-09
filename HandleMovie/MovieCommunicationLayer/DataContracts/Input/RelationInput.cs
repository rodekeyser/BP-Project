using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Input
{
    [DataContract]
    public class RelationInput: BaseInput
    {
        [DataMember]
        public int ParentId { get; set; }
        [DataMember]
        public int ChildId { get; set; }
        [DataMember]
        public string CharacterName { get; set; }
    }
}