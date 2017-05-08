using InvoiceDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDataLayer
{
    [Serializable]
    public class InvoiceHeader
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string VATNumber { get; set; }
        public int InvoiceNumber { get; set; }
        public bool IsPaid { get; set; }

        #region relations

        public List<InvoiceLine> InvoiceLines { get; set; }

        #endregion
    }
}
