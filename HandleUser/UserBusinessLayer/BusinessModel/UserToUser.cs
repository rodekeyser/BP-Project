using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBusinessLayer.BusinessModel.CriteriaObjects;
using UserBusinessLayer.BusinessModel.Editables;

namespace UserBusinessLayer.BusinessModel
{
    [Serializable]
    public class UserToUser : BusinessBase<UserToUser>
    {
        public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);
        public int UserId
        {
            get { return GetProperty(UserIdProperty); }
            set { SetProperty(UserIdProperty, value); }
        }
        public static readonly PropertyInfo<int> FriendIdProperty = RegisterProperty<int>(c => c.FriendId);
        public int FriendId
        {
            get { return GetProperty(FriendIdProperty); }
            set { SetProperty(FriendIdProperty, value); }
        }
        public static readonly PropertyInfo<EditableUser> UserProperty = RegisterProperty<EditableUser>(c => c.User);
        public EditableUser User
        {
            get { return GetProperty(UserProperty); }
            set { SetProperty(UserProperty, value); }
        }
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(UserIdProperty, "Please select a user"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(FriendIdProperty, "Please select a friend"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinValue<int>(UserIdProperty, 1,"Please select a user"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinValue<int>(FriendIdProperty, 1,"Please select a friend"));
        }
        public static UserToUser AddUserToUser(RelationsCriteria criteria)
        {
            return DataPortal.Create<UserToUser>(criteria);
        }
        public static void DeleteUserFromUser(RelationsCriteria criteria)
        {
            DataPortal.Delete<UserToUser>(criteria);
        }

        protected void DataPortal_Create(RelationsCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.UserId = criteria.ParentId;
                this.FriendId = criteria.ChildId;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            using (var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var user = context.User.Find(this.UserId);
                var friend = context.User.Find(this.FriendId);
                if (user == null)
                {
                    throw new Exception("There was no user found");
                }
                if (friend == null)
                {
                    throw new Exception("There was no friend found");
                }
                if(user.Friends == null)
                {
                    user.Friends = new List<UserDataLayer.DataModels.User>();
                }
                if(friend.Friends == null)
                {
                    friend.Friends = new List<UserDataLayer.DataModels.User>();
                }
                if (!user.Friends.Contains(friend))
                {
                    user.Friends.Add(friend);
                    friend.Friends.Add(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("You already have this user as a friend");
                }
                this.User = EditableUser.GetUserById(UserId);
            }
        }

        private void DataPortal_Delete(RelationsCriteria criteria)
        {
            using (var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var user = context.User.Find(criteria.ParentId);
                var friend = context.User.Find(criteria.ChildId);
                if (user == null)
                {
                    throw new Exception("There was no user found");
                }
                if (friend == null)
                {
                    throw new Exception("There was no friend found");
                }
                if (friend.Friends == null || user.Friends == null)
                    throw new Exception("You have no friends yet");
                user.Friends.Remove(friend);
                friend.Friends.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
