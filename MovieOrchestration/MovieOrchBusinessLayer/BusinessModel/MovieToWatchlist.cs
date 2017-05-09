using Csla;
using MovieOrchBusinessLayer.BusinessModel.CriteriaObjects;
using MovieOrchBusinessLayer.BusinessModel.NamedObjects;
using MovieOrchBusinessLayer.BusinessModel.ReadOnlyLists;
using MovieOrchBusinessLayer.MovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel
{
    [Serializable]
    public class MovieToWatchlist: BusinessBase<MovieToWatchlist>
    {
        public static readonly PropertyInfo<int> MovieIdProperty = RegisterProperty<int>(c => c.MovieId);
        public int MovieId
        {
            get { return GetProperty(MovieIdProperty); }
            set { SetProperty(MovieIdProperty, value); }
        }
        public static readonly PropertyInfo<int> WatchlistIdProperty = RegisterProperty<int>(c => c.WatchlistId);
        public int WatchlistId
        {
            get { return GetProperty(WatchlistIdProperty); }
            set { SetProperty(WatchlistIdProperty, value); }
        }
        public static readonly PropertyInfo<NamedWatchlist> WatchlistProperty = RegisterProperty<NamedWatchlist>(c => c.Watchlist);
        public NamedWatchlist Watchlist
        {
            get { return GetProperty(WatchlistProperty); }
            set { SetProperty(WatchlistProperty, value); }
        }
        public static readonly PropertyInfo<NamedExceptionReadOnlyList> ExceptionsProperty = RegisterProperty<NamedExceptionReadOnlyList>(c => c.Exceptions);
        public NamedExceptionReadOnlyList Exceptions
        {
            get { return GetProperty(ExceptionsProperty); }
            set { SetProperty(ExceptionsProperty, value); }
        }

        public static MovieToWatchlist AddMovieToWatchlist(RelationsCriteria criteria)
        {
            return DataPortal.Create<MovieToWatchlist>(criteria);
        }
        public static void RemoveMovieFromWatchlist(RelationsCriteria criteria)
        {
            DataPortal.Delete<MovieToWatchlist>(criteria);
        }

        protected void DataPortal_Create(RelationsCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.WatchlistId = criteria.ParentId;
                this.MovieId = criteria.ChildId;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            WatchlistResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.AddMovieToWatchList(new RelationInput()
                {
                    ChildId = this.MovieId,
                    ParentId = this.WatchlistId,
                    Application = "movie",
                    Password = "movie"
                });
            }
            if (!response.Succes)
            {
                this.Exceptions = NamedExceptionReadOnlyList.ConvertErrorDataObject(response.Errors.ToList());
                return;
            }
            else
            {
                this.Watchlist = NamedWatchlist.GetWatchlistById(this.WatchlistId);
            }
        }

        private void DataPortal_Delete(RelationsCriteria criteria)
        {
            BaseResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.DeleteMovieFromWatchlist(new RelationInput()
                {
                    ChildId = criteria.ChildId,
                    ParentId = criteria.ParentId,
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
        }
    }
}
