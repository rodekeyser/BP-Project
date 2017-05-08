using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceBussinesLayer.BusinessModel
{
    [Serializable]
    public class InvoiceLineReadOnlyList:ReadOnlyListBase<InvoiceLineReadOnlyList, ReadOnlyInvoiceLine>
    {
        public static InvoiceLineReadOnlyList ConvertInvoiceLineDataObject(List<InvoiceDataLayer.DataModels.InvoiceLine> invoiceLines) {
            return DataPortal.Fetch<InvoiceLineReadOnlyList>(invoiceLines);
        }

        private void DataPortal_Fetch(List<InvoiceDataLayer.DataModels.InvoiceLine> invoiceLines)
        {
            // TODO: load values into object
            IsReadOnly = false;
            foreach (var item in invoiceLines)
            {
                this.Add(ReadOnlyInvoiceLine.ConvertAsChild(item));
            }
            IsReadOnly = true;
        }
    }
}
