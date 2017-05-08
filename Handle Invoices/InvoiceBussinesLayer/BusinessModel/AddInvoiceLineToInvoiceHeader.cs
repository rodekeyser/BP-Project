using Csla;
using InvoiceBussinesLayer.BusinessModel.CriteriaObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceBussinesLayer.BusinessModel
{
    [Serializable]
    public class AddInvoiceLineToInvoiceHeader: BusinessBase<AddInvoiceLineToInvoiceHeader>
    {
        public static readonly PropertyInfo<int> InvoiceHeaderIDProperty = RegisterProperty<int>(c => c.InvoiceHeaderID);
        public int InvoiceHeaderID
        {
            get { return GetProperty(InvoiceHeaderIDProperty); }
            set { SetProperty(InvoiceHeaderIDProperty, value); }
        }
        public static readonly PropertyInfo<decimal> PricePerUnitProperty = RegisterProperty<decimal>(c => c.PricePerUnit);
        public decimal PricePerUnit
        {
            get { return GetProperty(PricePerUnitProperty); }
            set { SetProperty(PricePerUnitProperty, value); }
        }
        public static readonly PropertyInfo<decimal> QuantityProperty = RegisterProperty<decimal>(c => c.Quantity);
        public decimal Quantity
        {
            get { return GetProperty(QuantityProperty); }
            set { SetProperty(QuantityProperty, value); }
        }
        public static readonly PropertyInfo<decimal> VATRateProperty = RegisterProperty<decimal>(c => c.VATRate);
        public decimal VATRate
        {
            get { return GetProperty(VATRateProperty); }
            set { SetProperty(VATRateProperty, value); }
        }
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public  string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(InvoiceHeaderIDProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(PricePerUnitProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(QuantityProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(VATRateProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinValue<decimal>(QuantityProperty, 1));
        }


        public static AddInvoiceLineToInvoiceHeader NewAddInvoiceLineHeader(AddInvoiceLineToHeaderCriteria criteria)
        {
            return DataPortal.Create<AddInvoiceLineToInvoiceHeader>(criteria);
        }
        
        protected void DataPortal_Create(AddInvoiceLineToHeaderCriteria criteria)
        {

            this.InvoiceHeaderID = criteria.ID;
            this.Quantity = criteria.Quantity;
            this.PricePerUnit = criteria.PricePerUnit;
            this.VATRate = criteria.VATRate;
            this.Description = criteria.Description;
            base.DataPortal_Create();
        }
        protected override void DataPortal_Insert()
        {
            // TODO: insert object's data
            using (var context = new InvoiceDataLayer.DbContext.InvoiceLayerDBContext())
            {

                var invoiceHeader = context.InvoiceHeader.Include("InvoiceLines").Where(x => x.Id == this.InvoiceHeaderID).FirstOrDefault();
                var invoiceLineToCreate = new InvoiceDataLayer.DataModels.InvoiceLine();
                
                invoiceLineToCreate.PricePerUnit = this.PricePerUnit;
                invoiceLineToCreate.VATRate = this.VATRate;
                invoiceLineToCreate.Amount = this.Quantity;
                invoiceLineToCreate.VATAmount = invoiceLineToCreate.Amount / 100 * this.VATRate;
                invoiceLineToCreate.LineAmount = invoiceLineToCreate.Amount + invoiceLineToCreate.VATAmount;
                invoiceLineToCreate.Description = this.Description;
                if (invoiceHeader.InvoiceLines.Count > 0) 
                    invoiceHeader.InvoiceLines.Add(invoiceLineToCreate);
                else
                {
                    invoiceHeader.InvoiceLines = new List<InvoiceDataLayer.DataModels.InvoiceLine>();
                    invoiceHeader.InvoiceLines.Add(invoiceLineToCreate);
                }
                invoiceHeader.Amount = invoiceHeader.InvoiceLines.Sum(x => x.Amount);
                invoiceHeader.VATAmount = invoiceHeader.InvoiceLines.Sum(x => x.VATAmount);
                invoiceHeader.TotalAmount = invoiceHeader.VATAmount + invoiceHeader.Amount;
                context.SaveChanges();
            }

        }

        protected override void DataPortal_Update()
        {
            // TODO: update object's data

        }
        

    }
}
