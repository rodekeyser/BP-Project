using Csla;
using MovieOrchBusinessLayer.BusinessModel.CriteriaObjects;
using MovieOrchBusinessLayer.BusinessModel.NamedLists;
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
    public class NamedAchievement: BusinessBase<NamedAchievement>
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
        public static readonly PropertyInfo<NamedUserList> UsersProperty = RegisterProperty<NamedUserList>(c => c.Users);
        public NamedUserList Users
        {
            get { return GetProperty(UsersProperty); }
            set { SetProperty(UsersProperty, value); }
        }
        public static readonly PropertyInfo<NamedExceptionReadOnlyList> ExceptionsProperty = RegisterProperty<NamedExceptionReadOnlyList>(c => c.Exceptions);
        public NamedExceptionReadOnlyList Exceptions
        {
            get { return GetProperty(ExceptionsProperty); }
            set { SetProperty(ExceptionsProperty, value); }
        }

        public static NamedAchievement NewNamedAchievement(AchievementCriteria criteria)
        {
            return DataPortal.Create<NamedAchievement>(criteria);
        }

        public static NamedAchievement ConvertChild(AchievementResponse response)
        {
            return DataPortal.FetchChild<NamedAchievement>(response);
        }

        public static NamedAchievement GetAchievementById(int id)
        {
            return DataPortal.Fetch<NamedAchievement>(id);
        }

        public static void DeleteAchievementById(int id)
        {
            DataPortal.Delete<NamedAchievement>(id);
        }

        protected void DataPortal_Create(AchievementCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Description = criteria.Description;
                this.Name = criteria.Name;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            AchievementInput achievement = new AchievementInput();
            achievement.Description = this.Description;
            achievement.Name = this.Name;
            achievement.Application = "user";
            achievement.Password = "user";
            IdentificationResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.CreateAchievement(achievement);
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
            AchievementResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.GetAchievementById(new IdentificationInput() {
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
                this.Id = response.Id;
                this.Name = response.Name;
                this.Description = response.Description;
                if (response.Users != null && response.Users.ToList().Count > 0)
                    this.Users = NamedUserList.ConvertChild(response.Users.ToList());
            }
        }

        private void Child_Fetch(AchievementResponse response)
        {
            this.Id = response.Id;
            this.Name = response.Name;
            MarkAsChild();
        }

        private void DataPortal_Delete(int id)
        {
            BaseResponse response;
            using (var service = new UserServiceClient())
            {
                response = service.DeleteAchievementById(new IdentificationInput() {
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
            AchievementResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.UpdateAchievement(new AchievementInput()
                {
                    Id = this.Id,
                    Description = this.Description,
                    Name = this.Name,
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
                this.Name = response.Name;
                this.Description = response.Description;
            }
        }
    }
}
