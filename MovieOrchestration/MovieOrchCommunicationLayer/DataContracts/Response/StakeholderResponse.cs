using MovieOrchBusinessLayer.MovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieOrchCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class StakeholderResponse: BaseResponse
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string CharacterName { get; set; }
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
        [DataMember]
        public List<MovieResponse> Movies { get; set; }
        public StakeholderResponse()
        {
            this.Movies = new List<MovieResponse>();
        }
    }
}