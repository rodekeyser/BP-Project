using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.NamedObjects
{
    [Serializable]
    public class NamedException : BusinessBase<NamedException>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> ErrorMessageProperty = RegisterProperty<string>(c => c.ErrorMessage);
        public string ErrorMessage
        {
            get { return GetProperty(ErrorMessageProperty); }
            set { SetProperty(ErrorMessageProperty, value); }
        }

        public static readonly PropertyInfo<string> ErrorOriginProperty = RegisterProperty<string>(c => c.ErrorOrigin);
        public string ErrorOrigin
        {
            get { return GetProperty(ErrorOriginProperty); }
            set { SetProperty(ErrorOriginProperty, value); }
        }

        public static readonly PropertyInfo<string> PropertyNameProperty = RegisterProperty<string>(c => c.PropertyName);
        public string PropertyName
        {
            get { return GetProperty(PropertyNameProperty); }
            set { SetProperty(PropertyNameProperty, value); }
        }

        public static NamedException ConvertAsChild(UserService.Error toMark)
        {
            return DataPortal.FetchChild<NamedException>(toMark);
        }

        public static NamedException ConvertAsChild(MovieService.Error toMark)
        {
            return DataPortal.FetchChild<NamedException>(toMark);
        }

        public static NamedException NewNamedException()
        {
            return DataPortal.Create<NamedException>();
        }

        private void Child_Fetch(UserService.Error toMark)
        {
            this.ErrorMessage = toMark.ErrorMessage;
            this.PropertyName = toMark.PropertyName;
            this.ErrorOrigin = "User";
        }
        
        private void Child_Fetch(MovieService.Error toMark)
        {
            this.ErrorMessage = toMark.ErrorMessage;
            this.PropertyName = toMark.PropertyName;
            this.ErrorOrigin = "GameBoard";
        }

    }
}
