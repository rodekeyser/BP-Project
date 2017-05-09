using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieOrchCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class AchievementListResponse: BaseResponse
    {
        [DataMember]
        public List<AchievementResponse> Achievements { get; set; }
        public AchievementListResponse()
        {
            this.Achievements = new List<AchievementResponse>();
        }
    }
}