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
    public class AchievementToUser:BusinessBase<AchievementToUser>
    {
        public static readonly PropertyInfo<int> AchievIdProperty = RegisterProperty<int>(c => c.AchievId);
        public int AchievId
        {
            get { return GetProperty(AchievIdProperty); }
            set { SetProperty(AchievIdProperty, value); }
        }
        public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);
        public int UserId
        {
            get { return GetProperty(UserIdProperty); }
            set { SetProperty(UserIdProperty, value); }
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
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(AchievIdProperty, "Please select an achievement"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(UserIdProperty, "Please select a user"));
        }
        public static AchievementToUser AddAchievementToUser(RelationsCriteria criteria)
        {
            return DataPortal.Create<AchievementToUser>(criteria);
        }

        protected void DataPortal_Create(RelationsCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.UserId = criteria.ParentId;
                this.AchievId = criteria.ChildId;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            using (var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var user = context.User.Find(this.UserId);
                var achiev = context.Achievement.Find(this.AchievId);
                if (user == null)
                {
                    throw new Exception("There was no user found");
                }
                if (achiev == null)
                {
                    throw new Exception("There was no achievement found");
                }
                if (user.Achievements == null)
                    user.Achievements = new List<UserDataLayer.DataModels.Achievement>();
                user.Achievements.Add(achiev);
                context.SaveChanges();
                this.User = EditableUser.GetUserById(UserId);
            }
        }
    }
}
