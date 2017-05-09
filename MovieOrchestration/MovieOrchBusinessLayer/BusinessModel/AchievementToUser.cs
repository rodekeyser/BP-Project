using Csla;
using MovieOrchBusinessLayer.BusinessModel.CriteriaObjects;
using MovieOrchBusinessLayer.BusinessModel.NamedObjects;
using MovieOrchBusinessLayer.BusinessModel.ReadOnlyLists;
using MovieOrchBusinessLayer.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel
{
    [Serializable]
    public class AchievementToUser: BusinessBase<AchievementToUser>
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
        public static readonly PropertyInfo<NamedUser> UserProperty = RegisterProperty<NamedUser>(c => c.User);
        public NamedUser User
        {
            get { return GetProperty(UserProperty); }
            set { SetProperty(UserProperty, value); }
        }

        public static readonly PropertyInfo<NamedExceptionReadOnlyList> ExceptionsProperty = RegisterProperty<NamedExceptionReadOnlyList>(c => c.Exceptions);
        public NamedExceptionReadOnlyList Exceptions
        {
            get { return GetProperty(ExceptionsProperty); }
            set { SetProperty(ExceptionsProperty, value); }
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
            UserResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.AddAchievementToUser(new RelationsInput()
                {
                    ChildId = this.AchievId,
                    ParentId = this.UserId,
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
                this.User = NamedUser.GetUserById(this.UserId);
            }
        }
    }
}
