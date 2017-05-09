using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class MovieResponse: BaseResponse
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Cover { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public decimal Score { get; set; }
        [DataMember]
        public Genre Genre { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Playtime { get; set; }
        [DataMember]
        public List<ChildStakeholderResponse> Cast { get; set; }
        [DataMember]
        public ChildStakeholderResponse Director { get; set; }
        public MovieResponse()
        {
            this.Cast = new List<ChildStakeholderResponse>();
        }
    }
}