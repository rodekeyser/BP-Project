using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class MovieListResponse: BaseResponse
    {
        [DataMember]
        public List<ChildMovieResponse> Movies { get; set; }
        public MovieListResponse()
        {
            this.Movies = new List<ChildMovieResponse>();
        }
    }
}