using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class WatchlistListResponse: BaseResponse
    {
        [DataMember]
        public List<WatchlistResponse> Watchlists { get; set; }
        public WatchlistListResponse()
        {
            this.Watchlists = new List<WatchlistResponse>();
        }
    }
}