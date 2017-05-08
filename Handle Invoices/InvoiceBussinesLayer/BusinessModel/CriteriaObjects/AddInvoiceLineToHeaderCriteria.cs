using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceBussinesLayer.BusinessModel.CriteriaObjects
{
    [Serializable]
    public class AddInvoiceLineToHeaderCriteria:CriteriaBase<AddInvoiceLineToHeaderCriteria>
    {
        public int ID { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Quantity { get; set; }
        public decimal VATRate { get; set; }
        public string Description { get; set; }
    }
}
