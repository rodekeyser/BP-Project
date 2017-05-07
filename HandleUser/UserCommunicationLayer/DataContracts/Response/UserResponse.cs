using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UserCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class UserResponse: ClientResponse
    {
        [DataMember]
        public List<AchievementResponse> Achievements { get; set; }
        [DataMember]
        public List<UserResponse> Friends { get; set; }
        [DataMember]
        public List<WatchlistResponse> Watchlists { get; set; }
        public UserResponse()
        {
            this.Achievements = new List<AchievementResponse>();
            this.Friends = new List<UserResponse>();
            this.Watchlists = new List<WatchlistResponse>();
        }
    }
}