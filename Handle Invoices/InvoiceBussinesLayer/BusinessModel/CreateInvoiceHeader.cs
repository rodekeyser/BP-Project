using Csla;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceBussinesLayer.BusinessModel
{
    [Serializable]
    public class CreateInvoiceHeader: BusinessBase<CreateInvoiceHeader>
    {
        public static readonly PropertyInfo<int> IDProperty = RegisterProperty<int>(c => c.ID);
        public int ID
        {
            get { return GetProperty(IDProperty);}
            set { SetProperty(IDProperty, value); }
        }
        public static readonly PropertyInfo<string> VATNumberProperty = RegisterProperty<string>(c => c.VATNumber);
        public string VATNumber
        {
            get { return GetProperty(VATNumberProperty); }
            set { SetProperty(VATNumberProperty, value); }
        }
        public static readonly PropertyInfo<int> InvoiceNumberProperty = RegisterProperty<int>(c => c.InvoiceNumber);
        public int InvoiceNumber
        {
            get { return GetProperty(InvoiceNumberProperty); }
            private set { LoadProperty(InvoiceNumberProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(VATNumberProperty));
            BusinessRules.AddRule(new ValidVATNumber());
        }
        private class ValidVATNumber: Csla.Rules.BusinessRule
        {
            protected override void Execute(RuleContext context)
            {
                var createInvoiceHeader = (CreateInvoiceHeader)context.Target;

                if (!createInvoiceHeader.VATNumber.StartsWith("BE0"))
                {
                    context.AddErrorResult("VATNumber must start with BE0");
                }
            }
        }
        public static CreateInvoiceHeader NewCreateInvoiceHeader(string vATNumber)
        {
            return DataPortal.Create<CreateInvoiceHeader>(
                vATNumber);

        }
        protected override void DataPortal_Insert()
        {
            var objectToInsert = new InvoiceDataLayer.InvoiceHeader();
            objectToInsert.InvoiceNumber = this.InvoiceNumber;
            objectToInsert.VATNumber = this.VATNumber;

            using (var context = new InvoiceDataLayer.DbContext.InvoiceLayerDBContext())
            {
                context.InvoiceHeader.Add(objectToInsert);
                context.SaveChanges();
                this.ID = objectToInsert.Id;
            }
        }
        protected override void DataPortal_Update()
        {
            using(var context = new InvoiceDataLayer.DbContext.InvoiceLayerDBContext())
            {
                var objectToUpdate = context.InvoiceHeader.Find(this.ID);
                objectToUpdate.InvoiceNumber = this.InvoiceNumber;
                objectToUpdate.VATNumber = this.VATNumber;
                context.SaveChanges();
                this.ID = objectToUpdate.Id;
            }
        }

        protected void DataPortal_Create(string vATNumber)
        {
            this.VATNumber = vATNumber;
            base.DataPortal_Create();
            using(var context = new InvoiceDataLayer.DbContext.InvoiceLayerDBContext())
            {
                var invoiceNumber = context.InvoiceNumber.Find(1);
                if(invoiceNumber != null)
                {
                    invoiceNumber.LastUsedNumber = invoiceNumber.LastUsedNumber + 1;
                    this.InvoiceNumber = invoiceNumber.LastUsedNumber;
                    context.SaveChanges();
                }
                else
                {
                    var newInvoiceNumber = new InvoiceDataLayer.DataModels.InvoiceNumber();
                    newInvoiceNumber.LastUsedNumber = 1;
                    this.InvoiceNumber = 1;
                    context.InvoiceNumber.Add(newInvoiceNumber);
                    context.SaveChanges();
                }
            }
        }
    }
}
