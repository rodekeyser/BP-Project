using Csla;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBusinessLayer.BusinessModel.CriteriaObjects;
using UserBusinessLayer.BusinessModel.Helpers;
using UserDataLayer.DataModels;

namespace UserBusinessLayer.BusinessModel.Editables
{
    [Serializable]
    public class EditableAdmin: BusinessBase<EditableAdmin>
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

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(UserNameProperty, "You need to give a user name"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(EmailProperty, "You need to give your email "));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(PasswordProperty, "You need to give a password"));
            BusinessRules.AddRule(new ValidEmailUserName());
        }

        private class ValidEmailUserName : Csla.Rules.BusinessRule
        {
            protected override void Execute(RuleContext context)
            {
                var admin = (EditableAdmin)context.Target;
                using (var contxt = new UserDataLayer.DbContext.UserDbContext())
                {
                    var admins = contxt.Admin.ToList();
                    if (admins == null) { }

                    else
                    {
                        foreach (var item in admins)
                        {
                            if (item.UserName == admin.UserName)
                            {
                                context.AddErrorResult("There is already an admin with this name");
                            }
                            if (item.Email == admin.Email)
                                context.AddErrorResult("There is already an admin with this email");
                        }
                    }
                }
            }
        }

        public static EditableAdmin GetAdminById(int id)
        {
            return DataPortal.Fetch<EditableAdmin>(id);
        }

        public static EditableAdmin NewEditableAdmin(ClientCriteria criteria)
        {
            return DataPortal.Create<EditableAdmin>(criteria);
        }

        public static EditableAdmin ConvertChild(Admin admin)
        {
            return DataPortal.FetchChild<EditableAdmin>(admin);
        }

        public static void DeleteAdminById(int id)
        {
            DataPortal.Delete<EditableAdmin>(id);
        }

        protected void DataPortal_Create(ClientCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.UserName = criteria.UserName;
                this.Email = criteria.Email;
                this.Age = criteria.Age;
                if(criteria.Password != null)
                {
                    this.Password = PasswordHelper.HashPassword(criteria.Password);
                }
            }
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(int id)
        {
            Admin admin;
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                admin = context.Admin.Find(id);
            }
            if (admin == null)
                throw new Exception("There was no admin found");
            this.Id = id;
            this.Age = admin.Age;
            this.Email = admin.Email;
            this.Password = admin.Password;
            this.UserName = admin.UserName;
        }

        protected override void DataPortal_Insert()
        {
            Admin admin = new Admin();
            admin.Age = this.Age;
            admin.Email = this.Email;
            admin.Password = this.Password;
            admin.UserName = this.UserName;
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                context.Admin.Add(admin);
                context.SaveChanges();
            }
            this.Id = admin.Id;
        }

        private void Child_Fetch(Admin admin)
        {
            this.Id = admin.Id;
            this.UserName = admin.UserName;
            MarkAsChild();
        }

        private void DataPortal_Delete(int id)
        {
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var admin = context.Admin.Find(id);
                if (admin == null)
                    throw new Exception("There was no admin found");
                context.Admin.Remove(admin);
                context.SaveChanges();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var admin = context.Admin.Find(this.Id);
                if (admin == null)
                    throw new Exception("There was no admin found");
                admin.Age = this.Age;
                admin.Email = this.Email;
                admin.Password = this.Password;
                admin.UserName = this.UserName;
                context.SaveChanges();
            }
        }
    }
}
