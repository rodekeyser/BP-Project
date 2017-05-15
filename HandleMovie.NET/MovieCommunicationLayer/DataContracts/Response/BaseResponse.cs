using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Response
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember]
        public bool Succes { get; set; }
        [DataMember]
        public List<Error> Errors { get; set; }
        public BaseResponse()
        {
            this.Errors = new List<Error>();
        }
    }
    [DataContract]
    public class Error
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string PropertyName { get; set; }
    }
}