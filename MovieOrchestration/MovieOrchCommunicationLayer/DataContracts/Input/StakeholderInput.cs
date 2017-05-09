using MovieOrchBusinessLayer.MovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieOrchCommunicationLayer.DataContracts.Input
{
    [DataContract]
    public class StakeholderInput : BaseInput
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string FamilyName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int CountOscars { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public Sex Sex { get; set; }
    }
}