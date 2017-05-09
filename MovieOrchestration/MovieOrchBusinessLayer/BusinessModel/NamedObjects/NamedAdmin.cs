using Csla;
using MovieOrchBusinessLayer.BusinessModel.CriteriaObjects;
using MovieOrchBusinessLayer.BusinessModel.ReadOnlyLists;
using MovieOrchBusinessLayer.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.NamedObjects
{
    [Serializable]
    public class NamedAdmin: BusinessBase<NamedAdmin>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }
        public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(c => c.UserName);
        public string UserName
        {
            get { return GetProperty(UserNameProperty); }
            set { SetProperty(UserNameProperty, value); }
        }
        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);
        public string Email
        {
            get { return GetProperty(EmailProperty); }
            set { SetProperty(EmailProperty, value); }
        }
        public static readonly PropertyInfo<int> AgeProperty = RegisterProperty<int>(c => c.Age);
        public int Age
        {
            get { return GetProperty(AgeProperty); }
            set { SetProperty(AgeProperty, value); }
        }
        public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(c => c.Password);
        public string Password
        {
            get { return GetProperty(PasswordProperty); }
            set { SetProperty(PasswordProperty, value); }
        }
        public static readonly PropertyInfo<NamedExceptionReadOnlyList> ExceptionsProperty = RegisterProperty<NamedExceptionReadOnlyList>(c => c.Exceptions);
        public NamedExceptionReadOnlyList Exceptions
        {
            get { return GetProperty(ExceptionsProperty); }
            set { SetProperty(ExceptionsProperty, value); }
        }

        public static NamedAdmin NewNamedAdmin(ClientCriteria criteria)
        {
            return DataPortal.Create<NamedAdmin>(criteria);
        }

        public static NamedAdmin ConvertChild(ClientResponse response)
        {
            return DataPortal.FetchChild<NamedAdmin>(response);
        }

        public static NamedAdmin GetAdminById(int id)
        {
            return DataPortal.Fetch<NamedAdmin>(id);
        }

        public static void DeleteAdminById(int id)
        {
            DataPortal.Delete<NamedAdmin>(id);
        }

        protected void DataPortal_Create(ClientCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Age = criteria.Age;
                this.Email = criteria.Email;
                this.UserName = criteria.UserName;
                this.Password = criteria.Password; 
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            ClientInput admin = new ClientInput();
            admin.Age = this.Age;
            admin.ClientPassword = this.Password;
            admin.UserName = this.UserName;
            admin.Email = this.Email;
            admin.Password = "user";
            admin.Application = "user";
            IdentificationResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.CreateAdmin(admin);
                this.Id = response.Id;
            }
            if (!response.Succes)
            {
                this.Exceptions = NamedExceptionReadOnlyList.ConvertErrorDataObject(response.Errors.ToList());
                return;
            }
        }

        private void DataPortal_Fetch(int id)
        {
            ClientResponse response;
            using (var service = new UserServiceClient())
            {
                response = service.GetAdminById(new IdentificationInput()
                {
                    Id = id,
                    Application = "user",
                    Password = "user"
                });
            }
            if (!response.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in response.Errors)
                {
                    message.Append(item.ErrorMessage);
                }
                throw new Exception(message.ToString());
            }
            else
            {
                this.Id = id;
                this.Age = response.Age;
                this.Email = response.Email;
                this.Password = response.Password;
                this.UserName = response.UserName;
            }
        }

        private void Child_Fetch(ClientResponse response)
        {
            this.Id = response.Id;
            this.UserName = response.UserName;
            MarkAsChild();
        }

        private void DataPortal_Delete(int id)
        {
            BaseResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.DeleteAdminById(new IdentificationInput()
                {
                   Id = id,
                   Application = "user",
                   Password = "user" 
                });
            }
            if (!response.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in response.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
        }

        protected override void DataPortal_Update()
        {
            ClientResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.UpdateAdmin(new ClientInput() {
                    Id = this.Id,
                    Age = this.Age,
                    ClientPassword = this.Password,
                    Email = this.Email,
                    UserName = this.UserName,
                    Application = "user",
                    Password = "user"
                });
            }
            if (!response.Succes)
            {
                this.Exceptions = NamedExceptionReadOnlyList.ConvertErrorDataObject(response.Errors.ToList());
                return;
            }
            else
            {
                this.Id = response.Id;
                this.Age = response.Age;
                this.Email = response.Email;
                this.Password = response.Password;
                this.UserName = response.UserName;
            }
        }
    }
}
