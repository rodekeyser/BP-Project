using Csla;
using MovieOrchBusinessLayer.BusinessModel.NamedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.ReadOnlyLists
{
    [Serializable]
    public class NamedExceptionReadOnlyList : ReadOnlyListBase<NamedExceptionReadOnlyList, NamedException>
    {
        public static NamedExceptionReadOnlyList ConvertErrorDataObject(List<UserService.Error> exceptions)
        {
            return DataPortal.Fetch<NamedExceptionReadOnlyList>(exceptions);
        }

        public static NamedExceptionReadOnlyList ConvertErrorDataObject(List<MovieService.Error> exceptions)
        {
            return DataPortal.Fetch<NamedExceptionReadOnlyList>(exceptions);
        }

        private void DataPortal_Fetch(List<UserService.Error> exceptions)
        {
            IsReadOnly = false;

            foreach (var item in exceptions)
            {
                this.Add(NamedException.ConvertAsChild(item));
            }
            IsReadOnly = true;
        }
        
        private void DataPortal_Fetch(List<MovieService.Error> exceptions)
        {
            IsReadOnly = false;

            foreach (var item in exceptions)
            {
                this.Add(NamedException.ConvertAsChild(item));
            }
            IsReadOnly = true;
        }
    }
}
