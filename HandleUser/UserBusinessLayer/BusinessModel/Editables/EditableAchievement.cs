using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBusinessLayer.BusinessModel.CriteriaObjects;
using UserBusinessLayer.BusinessModel.EditableLists;
using UserDataLayer.DataModels;

namespace UserBusinessLayer.BusinessModel.Editables
{
    [Serializable]
    public class EditableAchievement: BusinessBase<EditableAchievement>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }
        public static readonly PropertyInfo<EditableUserList> UsersProperty = RegisterProperty<EditableUserList>(c => c.Users);
        public EditableUserList Users
        {
            get { return GetProperty(UsersProperty); }
            set { SetProperty(UsersProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(NameProperty, "You need to give a name"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(DescriptionProperty, "You need to give a description "));
        }

        public static EditableAchievement NewEditableAchievement(AchievementCriteria criteria)
        {
            return DataPortal.Create<EditableAchievement>(criteria);
        }

        public static EditableAchievement GetAchievementById(int id)
        {
            return DataPortal.Fetch<EditableAchievement>(id);
        }

        public static EditableAchievement ConvertChild(Achievement achiev)
        {
            return DataPortal.FetchChild<EditableAchievement>(achiev);
        }

        public static void DeleteAchievementById(int id)
        {
            DataPortal.Delete<EditableAchievement>(id);
        }

        protected void DataPortal_Create(AchievementCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Name = criteria.Name;
                this.Description = criteria.Description;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            Achievement achievement = new Achievement();
            achievement.Description = this.Description;
            achievement.Name = this.Name;
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                context.Achievement.Add(achievement);
                context.SaveChanges();
            }
            this.Id = achievement.Id;
        }

        private void DataPortal_Fetch(int id)
        {
            Achievement achievement;
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                achievement = context.Achievement.Include("Users").Where(x => x.Id == id).FirstOrDefault();
            }
            if (achievement == null)
                throw new Exception("There was no achievement found");
            this.Id = achievement.Id;
            this.Name = achievement.Name;
            this.Description = achievement.Description;
            if(achievement.Users != null)
            this.Users = EditableUserList.ConvertChild(achievement.Users);
        }

        private void Child_Fetch(Achievement achiev)
        {
            this.Id = achiev.Id;
            this.Name = achiev.Name;
            MarkAsChild();
        }

        private void DataPortal_Delete(int id)
        {
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var achiev = context.Achievement.Find(id);
                if (achiev == null)
                    throw new Exception("There was no achievement found");
                context.Achievement.Remove(achiev);
                context.SaveChanges();
            }
        }

        protected override void DataPortal_Update()
        {
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                var achiev = context.Achievement.Find(this.Id);
                if (achiev == null)
                    throw new Exception("There was no achievement found");
                achiev.Name = this.Name;
                achiev.Description = this.Description;
                context.SaveChanges();
            }
        }
    }
}
