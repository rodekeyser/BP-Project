using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceBussinesLayer.BusinessModel
{
    [Serializable]
    public class GetInvoiceHeader:ReadOnlyBase<GetInvoiceHeader>
    {
        public static readonly PropertyInfo<int> IDProperty = RegisterProperty<int>(c => c.ID);
        public int ID
        {
            get { return GetProperty(IDProperty); }
            private set { LoadProperty(IDProperty, value); }
        }
        public static readonly PropertyInfo<decimal> AmountProperty = RegisterProperty<decimal>(c => c.Amount);
        public decimal Amount
        {
            get { return GetProperty(AmountProperty); }
            private set { LoadProperty(AmountProperty, value); }
        }
        public static readonly PropertyInfo<string> VATNumberProperty = RegisterProperty<string>(c => c.VATNumber);
        public string VATNumber
        {
            get { return GetProperty(VATNumberProperty); }
            private set { LoadProperty(VATNumberProperty, value); }
        }
        public static readonly PropertyInfo<int> InvoiceNumberProperty = RegisterProperty<int>(c => c.InvoiceNumber);
        public int InvoiceNumber
        {
            get { return GetProperty(InvoiceNumberProperty); }
            private set { LoadProperty(InvoiceNumberProperty, value); }
        }
        #region FactoryMethods 

        public static GetInvoiceHeader GetGetInvoiceHeader(int id)
        {
            return DataPortal.Fetch<GetInvoiceHeader>(id);
        }
        public static readonly PropertyInfo<decimal> VATAmountProperty = RegisterProperty<decimal>(c => c.VATAmount);
        public decimal VATAmount
        {
            get { return GetProperty(VATAmountProperty); }
            private set { LoadProperty(VATAmountProperty, value); }
        }
        public static readonly PropertyInfo<decimal> TotalAmountProperty = RegisterProperty<decimal>(c => c.TotalAmount);
        public decimal TotalAmount
        {
            get { return GetProperty(TotalAmountProperty); }
            private set { LoadProperty(TotalAmountProperty, value); }
        }
        public static readonly PropertyInfo<bool> IsPaidProperty = RegisterProperty<bool>(c => c.IsPaid);
        public bool IsPaid
        {
            get { return GetProperty(IsPaidProperty); }
            private set { LoadProperty(IsPaidProperty, value); }
        }
        public static readonly PropertyInfo<InvoiceLineReadOnlyList> InvoiceLinesProperty = RegisterProperty<InvoiceLineReadOnlyList>(c => c.InvoiceLines);
        public InvoiceLineReadOnlyList InvoiceLines
        {
            get { return GetProperty(InvoiceLinesProperty); }
            private set { LoadProperty(InvoiceLinesProperty, value); }
        }
        #endregion

        #region DataPortal


        private void DataPortal_Fetch(int id)
        {
            // TODO: load values into object
            var toLoad = new InvoiceDataLayer.InvoiceHeader();
            using(var context = new InvoiceDataLayer.DbContext.InvoiceLayerDBContext())
            {
                toLoad = context.InvoiceHeader.Include("InvoiceLines").Where(x => x.Id == id).FirstOrDefault();
            }
            ID = toLoad.Id;
            this.Amount = toLoad.Amount;
            this.VATAmount = toLoad.VATAmount;
            this.VATNumber = toLoad.VATNumber;
            this.TotalAmount = toLoad.TotalAmount;
            this.IsPaid = toLoad.IsPaid;
            if (toLoad.InvoiceLines != null && toLoad.InvoiceLines.Count > 0)
                this.InvoiceLines = InvoiceLineReadOnlyList.ConvertInvoiceLineDataObject(toLoad.InvoiceLines);
        }



        #endregion
    }
}
