using InvoiceDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDataLayer.DataModels
{
    [Serializable]
    public class InvoiceException
    {
        public int Id { get; set; }
        public InvoiceExceptionTypes Exception { get; set; } = InvoiceExceptionTypes.None;
    }
}
