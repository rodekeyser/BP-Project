using Csla;
using MovieOrchBusinessLayer.BusinessModel.CriteriaObjects;
using MovieOrchBusinessLayer.BusinessModel.NamedLists;
using MovieOrchBusinessLayer.BusinessModel.ReadOnlyLists;
using MovieOrchBusinessLayer.MovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.NamedObjects
{
    [Serializable]
    public class NamedWatchlist : BusinessBase<NamedWatchlist>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }
        public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);
        public int UserId
        {
            get { return GetProperty(UserIdProperty); }
            set { SetProperty(UserIdProperty, value); }
        }
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }
        public static readonly PropertyInfo<NamedMovieList> MoviesProperty = RegisterProperty<NamedMovieList>(c => c.Movies);
        public NamedMovieList Movies
        {
            get { return GetProperty(MoviesProperty); }
            set { SetProperty(MoviesProperty, value); }
        }
        public static readonly PropertyInfo<NamedExceptionReadOnlyList> ExceptionsProperty = RegisterProperty<NamedExceptionReadOnlyList>(c => c.Exceptions);
        public NamedExceptionReadOnlyList Exceptions
        {
            get { return GetProperty(ExceptionsProperty); }
            set { SetProperty(ExceptionsProperty, value); }
        }

        public static NamedWatchlist NewNamedWatchlist(WatchlistCriteria criteria)
        {
            return DataPortal.Create<NamedWatchlist>(criteria);
        }

        public static NamedWatchlist ConvertChild(WatchlistResponse response)
        {
            return DataPortal.FetchChild<NamedWatchlist>(response);
        }

        public static NamedWatchlist GetWatchlistById(int id)
        {
            return DataPortal.Fetch<NamedWatchlist>(id);
        }

        public static void DeleteWatchlistById(int id)
        {
            DataPortal.Delete<NamedWatchlist>(id);
        }

        protected void DataPortal_Create(WatchlistCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Name = criteria.Name;
                this.UserId = criteria.UserId;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            MovieService.WatchlistInput wlMovie = new MovieService.WatchlistInput();
            wlMovie.Name = this.Name;
            wlMovie.Application = "movie";
            wlMovie.Password = "movie";
            MovieService.IdentificationResponse responseMovie;
            using (var service = new MovieService.MovieServiceClient())
            {
                responseMovie = service.CreateWatchlist(wlMovie);
            }
            if (!responseMovie.Succes)
            {
                this.Exceptions = NamedExceptionReadOnlyList.ConvertErrorDataObject(responseMovie.Errors.ToList());
                return;
            }
            else
            {
                UserService.WatchlistInput wlUser = new UserService.WatchlistInput();
                wlUser.Id = responseMovie.Id;
                wlUser.UserId = this.UserId;
                wlUser.Application = "user";
                wlUser.Password = "user";
                UserService.IdentificationResponse responseUser;
                using (var service = new UserService.UserServiceClient())
                {
                    responseUser = service.AddWatchListToUser(wlUser);
                    this.Id = responseUser.Id;
                }
                if (!responseUser.Succes)
                {
                    this.Exceptions = NamedExceptionReadOnlyList.ConvertErrorDataObject(responseUser.Errors.ToList());
                    return;
                }
            }
        }

        private void DataPortal_Fetch(int id)
        {
            WatchlistResponse response;
            using (var service = new MovieServiceClient())
            {
                response = service.GetWatchlistById(new IdentificationInput()
                {
                    Id = id,
                    Application = "movie",
                    Password = "movie"
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

                if (response.Movies != null && response.Movies.ToList().Count > 0)
                {
                    this.Movies = NamedMovieList.ConvertChild(response.Movies.ToList());
                }
            }
        }

        private void Child_Fetch(WatchlistResponse response)
        {
            this.Id = response.Id;
            this.Name = response.Name;

            if (response.Movies != null && response.Movies.ToList().Count > 0)
            {
                this.Movies = NamedMovieList.ConvertChild(response.Movies.ToList());
            }

            MarkAsChild();
        }

        private void DataPortal_Delete(int id)
        {
            BaseResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.DeleteWatchlistById(new IdentificationInput() {
                    Id = id,
                    Application = "movie",
                    Password = "movie"
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
            UserService.BaseResponse userResponse;
            using (var service = new UserService.UserServiceClient())
            {
                userResponse = service.DeleteWatchlistById(new UserService.IdentificationInput()
                {
                    Id = id,
                    Application = "user",
                    Password = "user"
                });
            }
            if (!userResponse.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in userResponse.Errors)
                {
                    message.Append(item.ErrorMessage);
                }
                throw new Exception(message.ToString());
            }
        }

        protected override void DataPortal_Update()
        {
            WatchlistResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.UpdateWatchlist(new WatchlistInput()
                {
                    Id = this.Id,
                    Name = this.Name,
                    Application = "movie",
                    Password = "movie"
                });
                if (!response.Succes)
                {
                    this.Exceptions = NamedExceptionReadOnlyList.ConvertErrorDataObject(response.Errors.ToList());
                    return;
                }
                else
                {
                    this.Id = response.Id;
                    this.Name = response.Name;
                }
            }
        }
    }
}
