using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceBussinesLayer.BusinessModel
{
    [Serializable]
    public class ReadOnlyInvoiceLine:ReadOnlyBase<ReadOnlyInvoiceLine>
    {
        public static readonly PropertyInfo<decimal> AmountProperty = RegisterProperty<decimal>(c => c.Amount);
        public decimal Amount
        {
            get { return GetProperty(AmountProperty); }
            private set { LoadProperty(AmountProperty, value); }
        }
        public static readonly PropertyInfo<int> IDProperty = RegisterProperty<int>(c => c.ID);
        public int ID
        {
            get { return GetProperty(IDProperty); }
            private set { LoadProperty(IDProperty, value); }
        }
        public static readonly PropertyInfo<decimal> LineAmountProperty = RegisterProperty<decimal>(c => c.LineAmount);
        public decimal LineAmount
        {
            get { return GetProperty(LineAmountProperty); }
            private set { LoadProperty(LineAmountProperty, value); }
        }
        public static readonly PropertyInfo<decimal> VATAmountProperty = RegisterProperty<decimal>(c => c.VATAmount);
        public decimal VATAmount
        {
            get { return GetProperty(VATAmountProperty); }
            private set { LoadProperty(VATAmountProperty, value); }
        }
        public static readonly PropertyInfo<decimal> VATRateProperty = RegisterProperty<decimal>(c => c.VATRate);
        public decimal VATRate
        {
            get { return GetProperty(VATRateProperty); }
            private set { LoadProperty(VATRateProperty, value); }
        }
        public static readonly PropertyInfo<decimal> QuantityProperty = RegisterProperty<decimal>(c => c.Quantity);
        public decimal Quantity
        {
            get { return GetProperty(QuantityProperty); }
            private set { LoadProperty(QuantityProperty, value); }
        }
        public static readonly PropertyInfo<decimal> PricePerUnitProperty = RegisterProperty<decimal>(c => c.PricePerUnit);
        public decimal PricePerUnit
        {
            get { return GetProperty(PricePerUnitProperty); }
            private set { LoadProperty(PricePerUnitProperty, value); }
        }
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            private set { LoadProperty(DescriptionProperty, value); }
        }
        #region Factory

        public static ReadOnlyInvoiceLine ConvertAsChild(InvoiceDataLayer.DataModels.InvoiceLine toMark)
        {
            return DataPortal.FetchChild<ReadOnlyInvoiceLine>(toMark);
        }

        #endregion

        #region Data


        private void DataPortal_Fetch(int id)
        {
            // TODO: load values into object
            var toLoad = new InvoiceDataLayer.DataModels.InvoiceLine();
            using(var context = new InvoiceDataLayer.DbContext.InvoiceLayerDBContext())
            {
                toLoad = context.InvoiceLine.Find(id);
            }
            this.ID = toLoad.Id;
            this.VATAmount = toLoad.VATAmount;
            this.VATRate = toLoad.VATRate;
            this.Quantity = toLoad.Quantity;
            this.PricePerUnit = toLoad.PricePerUnit;
            this.LineAmount = toLoad.LineAmount;
            this.Description = toLoad.Description;
        }

        private void Child_Fetch(InvoiceDataLayer.DataModels.InvoiceLine toMark)
        {
            this.ID = toMark.Id;
            this.VATAmount = toMark.VATAmount;
            this.VATRate = toMark.VATRate;
            this.Quantity = toMark.Quantity;
            this.PricePerUnit = toMark.PricePerUnit;
            this.LineAmount = toMark.LineAmount;
            this.Description = toMark.Description;
        }
        #endregion
    }
}
