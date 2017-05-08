using InvoiceDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService1.DataContracts.Response
{
    [DataContract]
    public class GetInvoiceHeaderResponse
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
        [DataMember]
        public List<InvoiceLine> InvoiceLines { get; set; }

        public GetInvoiceHeaderResponse()
        {
            this.InvoiceLines = new List<InvoiceLine>();
        }
    }
}