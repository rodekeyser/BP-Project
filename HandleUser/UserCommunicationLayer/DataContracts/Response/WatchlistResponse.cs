using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UserCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class WatchlistResponse
    {
        [DataMember]
        public int Id { get; set; }
    }
}