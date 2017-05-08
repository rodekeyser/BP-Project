using Csla;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBusinessLayer.BusinessModel.CriteriaObjects;
using UserBusinessLayer.BusinessModel.EditableLists;
using UserBusinessLayer.BusinessModel.Helpers;
using UserDataLayer.DataModels;

namespace UserBusinessLayer.BusinessModel.Editables
{
    [Serializable]
    public class EditableUser: BusinessBase<EditableUser>
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
        public static readonly PropertyInfo<EditableWatchlistList> WatchlistsProperty = RegisterProperty<EditableWatchlistList>(c => c.Watchlists);
        public EditableWatchlistList Watchlists
        {
            get { return GetProperty(WatchlistsProperty); }
            set { SetProperty(WatchlistsProperty, value); }
        }
        public static readonly PropertyInfo<EditableAchievementList> AchievementsProperty = RegisterProperty<EditableAchievementList>(c => c.Achievements);
        public EditableAchievementList Achievements
        {
            get { return GetProperty(AchievementsProperty); }
            set { SetProperty(AchievementsProperty, value); }
        }
        public static readonly PropertyInfo<EditableUserList> FriendsProperty = RegisterProperty<EditableUserList>(c => c.Friends);
        public EditableUserList Friends
        {
            get { return GetProperty(FriendsProperty); }
            set { SetProperty(FriendsProperty, value); }
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
                var user = (EditableUser)context.Target;
                using (var contxt = new UserDataLayer.DbContext.UserDbContext())
                {
                    var users = contxt.User.ToList();
                    if (users == null) { }
                    else
                    {
                        foreach (var item in users)
                        {
                            if (item.UserName == user.UserName)
                            {
                                context.AddErrorResult("There is already a user with this name");
                            }
                            if (item.Email == user.Email)
                                context.AddErrorResult("There is already a user with this email");
                        }
                    }
                }
            }
        }

        public static EditableUser NewEditableUser(ClientCriteria criteria)
        {
            return DataPortal.Create<EditableUser>(criteria);
        }

        public static EditableUser ConvertChild(User user)
        {
            return DataPortal.FetchChild<EditableUser>(user);
        }

        public static EditableUser GetUserById(int id)
        {
            return DataPortal.Fetch<EditableUser>(id);
        }

        public static void DeleteUserById(int id)
        {
            DataPortal.Delete<EditableUser>(id);
        }

        protected void DataPortal_Create(ClientCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.UserName = criteria.UserName;
                this.Email = criteria.Email;
                this.Age = criteria.Age;
                if (criteria.Password != null)
                {
                    this.Password = PasswordHelper.HashPassword(criteria.Password);
                }
                else
                    this.Password = criteria.Password;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            User user = new User();
            user.Age = this.Age;
            user.Email = this.Email;
            user.Password = this.Password;
            user.UserName = this.UserName;
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                context.User.Add(user);
                context.SaveChanges();
            }
            this.Id = user.Id;
        }

        private void DataPortal_Fetch(int id)
        {
            User user;
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                user = context.User.Include("Achievements").Include("Watchlists").Include("Friends").Where(x => x.Id == id).FirstOrDefault();
            }
            if (user == null)
                throw new Exception("There was no user found");
            this.Id = id;
            this.Age = user.Age;
            this.Email = user.Email;
            this.Password = user.Password;
            this.UserName = user.UserName;
            if(user.Watchlists != null)
            {
                this.Watchlists = EditableWatchlistList.ConvertChild(user.Watchlists);
            }
            if(user.Achievements != null)
            {
                this.Achievements = EditableAchievementList.ConvertChild(user.Achievements);
            }
            if(user.Friends != null)
            {
                this.Friends = EditableUserList.ConvertChild(user.Friends);
            }
        }

        private void Child_Fetch(User user)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            MarkAsChild();
        }

        private void DataPortal_Delete(int id)
        {
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var user = context.User.Include("Watchlists").Where(x => x.Id == id).FirstOrDefault();
                if (user == null)
                    throw new Exception("There as no user found");
                if(user.Watchlists != null)
                {
                    List<Watchlist> wls = new List<Watchlist>();
                    wls.AddRange(user.Watchlists);
                    context.Watchlist.RemoveRange(wls);
                }
                context.User.Remove(user);
                context.SaveChanges();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var user = context.User.Find(this.Id);
                if (user == null)
                    throw new Exception("There was no user found");
                user.Age = this.Age;
                user.Email = this.Email;
                user.Password = this.Password;
                user.UserName = this.UserName;
                context.SaveChanges();
            }
        }
    }
}
