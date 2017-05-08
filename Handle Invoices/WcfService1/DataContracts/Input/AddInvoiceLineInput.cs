using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService1.DataContracts.Input
{
    [DataContract]
    public class AddInvoiceLineInput
    {
        [DataMember]
        public BaseInput Base { get; set; }
        [DataMember]
        public int InvoiceHeaderId { get; set; }
        [DataMember]
        public decimal PricePerUnit { get; set; }
        [DataMember]
        public decimal Quantity { get; set; }
        [DataMember]
        public decimal VATRate { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}