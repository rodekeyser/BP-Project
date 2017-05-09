using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieOrchCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class UserResponse: ClientResponse
    {
        [DataMember]
        public List<AchievementResponse> Achievements { get; set; }
        [DataMember]
        public List<ClientResponse> Friends { get; set; }
        public UserResponse()
        {
            this.Achievements = new List<AchievementResponse>();
            this.Friends = new List<ClientResponse>();
        }
    }
}