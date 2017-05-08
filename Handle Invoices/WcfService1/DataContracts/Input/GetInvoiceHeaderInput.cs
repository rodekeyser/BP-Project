using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService1.DataContracts.Input
{
    [DataContract]
    public class GetInvoiceHeaderInput
    {
        [DataMember]
        public BaseInput Base { get; set; }
        [DataMember]
        public int InvoiceHeaderId { get; set; }
    }
}