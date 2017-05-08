using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDataLayer.DataModels
{
    [Serializable]
    public class InvoiceLine
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public decimal LineAmount { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Quantity { get; set; }
        public decimal VATAmount { get; set; }
        public decimal VATRate { get; set; }
        public int Id { get; set; }

        #region relations

        public InvoiceHeader InvoiceHeader { get; set; }

        #endregion
    }
}
