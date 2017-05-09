using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieOrchCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class AchievementResponse: BaseResponse
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<ClientResponse> Users { get; set; }
        public AchievementResponse()
        {
            this.Users = new List<ClientResponse>();
        }
    }
}