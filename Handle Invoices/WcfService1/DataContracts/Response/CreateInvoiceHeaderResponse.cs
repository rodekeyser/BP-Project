using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WcfService1.DataContracts.Input;

namespace WcfService1.DataContracts.Response
{
    [DataContract]
    public class CreateInvoiceHeaderResponse
    {
        [DataMember]
        public BaseResponse Base { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public decimal VATAmount { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public string VATNumber { get; set; }
        [DataMember]
        public int InvoiceNumber { get; set; }
        [DataMember]
        public bool IsPaid { get; set; }
    }
}