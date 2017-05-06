using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class ChildMovieResponse
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
        public Genre Genre { get; set; }
        [DataMember]
        public int Playtime { get; set; }
    }
}