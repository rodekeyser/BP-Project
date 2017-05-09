using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieOrchCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class MovieListResponse: BaseResponse
    {
        [DataMember]
        public List<MovieResponse> Movies { get; set; }
        public MovieListResponse()
        {
            this.Movies = new List<MovieResponse>();
        }
    }
}