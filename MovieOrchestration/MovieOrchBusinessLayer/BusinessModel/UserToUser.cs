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
    public class UserToUser: BusinessBase<UserToUser>
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

        public static UserToUser AddFriend(RelationsCriteria criteria)
        {
            return DataPortal.Create<UserToUser>(criteria);
        }
        public static void RemoveFriend(RelationsCriteria criteria)
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
            UserResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.AddFriend(new RelationsInput()
                {
                    ChildId = this.FriendId,
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

        private void DataPortal_Delete(RelationsCriteria criteria)
        {
            BaseResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.DeleteFriend(new RelationsInput()
                {
                    ChildId = criteria.ChildId,
                    ParentId = criteria.ParentId,
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
    }
}
