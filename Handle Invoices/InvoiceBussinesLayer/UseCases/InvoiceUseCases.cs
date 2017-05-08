using InvoiceBussinesLayer.BusinessModel.CriteriaObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace InvoiceBussinesLayer.UseCases
{
    public class InvoiceUseCases
    {
        public BusinessModel.CreateInvoiceHeader UC_301_001_CreateInvoiceHeader(string vatNumber)
        {
            var invoiceHeaderToCreate = BusinessModel.CreateInvoiceHeader.NewCreateInvoiceHeader(vatNumber);
            if (invoiceHeaderToCreate.IsValid)
            {
                invoiceHeaderToCreate = invoiceHeaderToCreate.Save();
            }
            return invoiceHeaderToCreate;
        }
        public BusinessModel.AddInvoiceLineToInvoiceHeader UC_301_002_AddInvoiceLineToHeader(AddInvoiceLineToHeaderCriteria criteria)
        {
            var invoiceLineToAdd = BusinessModel.AddInvoiceLineToInvoiceHeader.NewAddInvoiceLineHeader(criteria);
            if (invoiceLineToAdd.IsValid)
            {
                invoiceLineToAdd = invoiceLineToAdd.Save();
            }
            return invoiceLineToAdd;
        }
        public BusinessModel.GetInvoiceHeader UC_301_003_GetInvoiceByID(int id)
        {
            return InvoiceBussinesLayer.BusinessModel.GetInvoiceHeader.GetGetInvoiceHeader(id);
        }
        public void ValidateIncomingRequest(string application, string password)
        {
            if (string.IsNullOrWhiteSpace(application))
            {
                throw new Exception("No application entered");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password");
            }
            if(application != WebConfigurationManager.AppSettings["Application"])
            {
                throw new Exception("Wrong application");
            }
            if(password != WebConfigurationManager.AppSettings["Password"])
            {
                throw new Exception("Wrong password");
            }
        }
    }
}
