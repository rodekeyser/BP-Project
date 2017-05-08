﻿using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class WatchlistResponse : BaseResponse
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<ChildMovieResponse> Movies { get; set; }
        public WatchlistResponse()
        {
            this.Movies = new List<ChildMovieResponse>();
        }
    }
}