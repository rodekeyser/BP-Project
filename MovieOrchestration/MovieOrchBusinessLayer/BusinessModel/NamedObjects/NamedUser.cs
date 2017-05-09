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
    public class NamedUser: BusinessBase<NamedUser>
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
        public static readonly PropertyInfo<NamedWatchlistList> WatchlistsProperty = RegisterProperty<NamedWatchlistList>(c => c.Watchlists);
        public NamedWatchlistList Watchlists
        {
            get { return GetProperty(WatchlistsProperty); }
            set { SetProperty(WatchlistsProperty, value); }
        }
        public static readonly PropertyInfo<NamedAchievementList> AchievementsProperty = RegisterProperty<NamedAchievementList>(c => c.Achievements);
        public NamedAchievementList Achievements
        {
            get { return GetProperty(AchievementsProperty); }
            set { SetProperty(AchievementsProperty, value); }
        }
        public static readonly PropertyInfo<NamedUserList> FriendsProperty = RegisterProperty<NamedUserList>(c => c.Friends);
        public NamedUserList Friends
        {
            get { return GetProperty(FriendsProperty); }
            set { SetProperty(FriendsProperty, value); }
        }
        public static readonly PropertyInfo<NamedExceptionReadOnlyList> ExceptionsProperty = RegisterProperty<NamedExceptionReadOnlyList>(c => c.Exceptions);
        public NamedExceptionReadOnlyList Exceptions
        {
            get { return GetProperty(ExceptionsProperty); }
            set { SetProperty(ExceptionsProperty, value); }
        }

        public static NamedUser NewNamedUser(ClientCriteria criteria)
        {
            return DataPortal.Create<NamedUser>(criteria);
        }

        public static NamedUser ConvertChild(ClientResponse response)
        {
            return DataPortal.FetchChild<NamedUser>(response);
        }

        public static NamedUser GetUserById(int id)
        {
            return DataPortal.Fetch<NamedUser>(id);
        }

        public static void DeleteUserById(int id)
        {
            DataPortal.Delete<NamedUser>(id);
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
            ClientInput user = new ClientInput();
            user.Age = this.Age;
            user.ClientPassword = this.Password;
            user.UserName = this.UserName;
            user.Email = this.Email;
            user.Password = "user";
            user.Application = "user";
            IdentificationResponse response;
            using (var service = new UserServiceClient())
            {
                response = service.CreateUser(user);
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
            UserResponse response;
            using (var service = new UserServiceClient())
            {
                response = service.GetUserById(new IdentificationInput()
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
                if (response.Achievements != null && response.Achievements.ToList().Count > 0)
                {
                    this.Achievements = NamedAchievementList.ConvertChild(response.Achievements.ToList());
                }
                if (response.Friends != null && response.Friends.ToList().Count > 0)
                {
                    this.Friends = NamedUserList.ConvertChild(response.Friends.ToList());
                }
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
            UserResponse user;
            using (var service = new UserServiceClient())
            {
                user = service.GetUserById(new IdentificationInput()
                {
                    Id = id,
                    Application = "user",
                    Password = "user"
                });
            }
            if (!user.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in user.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
            if (user.Watchlists != null && user.Watchlists.ToList().Count > 0)
            {
                using (var service = new MovieService.MovieServiceClient())
                {
                    foreach (var item in user.Watchlists)
                    {
                        MovieService.BaseResponse resp;
                        resp = service.DeleteWatchlistById(new MovieService.IdentificationInput() {
                            Id = item.Id,
                            Application = "movie",
                            Password = "movie"
                        });
                        if (!resp.Succes)
                        {
                            StringBuilder message = new StringBuilder();
                            foreach (var itemm in resp.Errors)
                            {
                                message.Append(itemm.ErrorMessage);

                            }
                            throw new Exception(message.ToString());
                        }
                    }
                }
            }
            BaseResponse response;
            using(var service = new UserServiceClient())
            {
                response = service.DeleteUserById(new IdentificationInput()
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
            using (var service = new UserServiceClient())
            {
                response = service.UpdateUser(new ClientInput()
                {
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
