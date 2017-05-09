using Csla;
using MovieBusinessLayer.BusinessModel.CriteriaObjects;
using MovieBusinessLayer.BusinessModel.Editables;
using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel
{
    [Serializable]
    public class MovieToWatchlist: BusinessBase<MovieToWatchlist>
    {
        public static readonly PropertyInfo<int> WatchlistIdProperty = RegisterProperty<int>(c => c.WatchlistId);
        public int WatchlistId
        {
            get { return GetProperty(WatchlistIdProperty); }
            set { SetProperty(WatchlistIdProperty, value); }
        }
        public static readonly PropertyInfo<int> MovieIdProperty = RegisterProperty<int>(c => c.MovieId);
        public int MovieId
        {
            get { return GetProperty(MovieIdProperty); }
            set { SetProperty(MovieIdProperty, value); }
        }
        public static readonly PropertyInfo<EditableWatchList> WatchlistProperty = RegisterProperty<EditableWatchList>(c => c.Watchlist);
        public EditableWatchList Watchlist
        {
            get { return GetProperty(WatchlistProperty); }
            set { SetProperty(WatchlistProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(MovieIdProperty, "Please select a movie"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(WatchlistIdProperty, "Please select a watchlist"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinValue<int>(MovieIdProperty,1, "Please select a movie"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinValue<int>(WatchlistIdProperty, 1,"Please select a watchlist"));
        }

        public static MovieToWatchlist AddMovieToWatchlist(RelationsCriteria criteria)
        {
            return DataPortal.Create<MovieToWatchlist>(criteria);
        }

        public static void DeleteMovieFromWatchlist(RelationsCriteria criteria)
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

        private void DataPortal_Delete(RelationsCriteria criteria)
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                var watchlist = context.Watchlist.Include("Movies").Where(x => x.Id == criteria.ParentId).FirstOrDefault();
                var movie = context.Movie.Find(criteria.ChildId);
                if (watchlist == null)
                    throw new Exception("There was no watchlist found");
                if (movie == null)
                    throw new Exception("There was no movie found");
                if (watchlist.Movies.Contains(movie))
                {
                    watchlist.Movies.Remove(movie);
                    context.SaveChanges();
                }
                else
                    throw new Exception("The watchlist does not contain this movie");
            }
        }

        protected override void DataPortal_Insert()
        {
            WatchList watchlist;
            Movie movie;
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                watchlist = context.Watchlist.Include("Movies").Where(x => x.Id == this.WatchlistId).FirstOrDefault();
                movie = context.Movie.Find(this.MovieId);
                if (watchlist == null)
                    throw new Exception("There was no watchlist found");
                if (movie == null)
                    throw new Exception("There was no movie found");
                if (!watchlist.Movies.Contains(movie))
                {
                    watchlist.Movies.Add(movie);
                    context.SaveChanges();
                }
                else
                    throw new Exception("The watchlist already contains this movie");
            }
            this.Watchlist = EditableWatchList.GetWatchlistById(this.WatchlistId);
        }
    }
}
