using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDataLayer.DataModels
{
    [Serializable]
    public sealed class InvoiceNumber
    {
        public int Id { get; set; }
        public int LastUsedNumber { get; set; } = 1;
        private static InvoiceNumber instance = null;
        private static readonly object padlock = new object();

        public InvoiceNumber() { }

        public static InvoiceNumber Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new InvoiceNumber();
                    }
                    return instance;
                }
            }
        }


    }
}
