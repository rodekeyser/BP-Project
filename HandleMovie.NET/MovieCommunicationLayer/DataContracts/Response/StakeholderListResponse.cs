using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class StakeholderListResponse: BaseResponse
    {
        [DataMember]
        public List<ChildStakeholderResponse> Stakeholders { get; set; }

        public StakeholderListResponse()
        {
            this.Stakeholders = new List<ChildStakeholderResponse>();
        }
    }
}