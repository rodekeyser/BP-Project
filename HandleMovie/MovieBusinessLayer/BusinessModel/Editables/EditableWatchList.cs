using Csla;
using MovieBusinessLayer.BusinessModel.CriteriaObjects;
using MovieBusinessLayer.BusinessModel.EditableLists;
using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel.Editables
{
    [Serializable]
    public class EditableWatchList: BusinessBase<EditableWatchList>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }
        public static readonly PropertyInfo<EditableMovieList> MoviesProperty = RegisterProperty<EditableMovieList>(c => c.Movies);
        public EditableMovieList Movies
        {
            get { return GetProperty(MoviesProperty); }
            set { SetProperty(MoviesProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(NameProperty, "You need to add a name"));
        }

        public static EditableWatchList NewWatchlist(WatchlistCriteria criteria)
        {
            return DataPortal.Create<EditableWatchList>(criteria);
        }

        public static void DeleteWatchlistById(int id)
        {
            DataPortal.Delete<EditableWatchList>(id);
        }

        public static EditableWatchList GetWatchlistById(int id)
        {
            return DataPortal.Fetch<EditableWatchList>(id);
        }

        public static EditableWatchList ConvertChild(WatchList watchList)
        {
            return DataPortal.FetchChild<EditableWatchList>(watchList);
        } 

        protected void DataPortal_Create(WatchlistCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Name = criteria.Name;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                WatchList wl = new WatchList() {
                    Name = this.Name
                };
                context.Watchlist.Add(wl);
                context.SaveChanges();
                this.Id = wl.Id;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                var watchList = context.Watchlist.Where(x => x.Id == this.Id).FirstOrDefault();
                watchList.Name = this.Name;
                context.SaveChanges();
            }
        }

        private void DataPortal_Delete(int id)
        {
            WatchList watchList;
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                watchList = context.Watchlist.Where(x => x.Id == id).FirstOrDefault();
                if (watchList == null)
                    throw new Exception("No watchlist was found");
                context.Watchlist.Remove(watchList);
                context.SaveChanges();
            }
        }

        private void DataPortal_Fetch(int id)
        {
            WatchList watchList;
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                watchList = context.Watchlist.Include("Movies").Where(x => x.Id == id).FirstOrDefault();
            }
            if (watchList == null)
                throw new Exception("No watchlist was found");
            this.Id = watchList.Id;
            this.Name = watchList.Name;

            if (watchList.Movies != null && watchList.Movies.Count > 0)
            {
                this.Movies = EditableMovieList.ConvertChild(watchList.Movies);
            }
        }

        private void Child_Fetch(WatchList watchList)
        {
            this.Id = watchList.Id;
            this.Name = watchList.Name;
            
            if(watchList.Movies != null && watchList.Movies.Count > 0)
            {
                this.Movies = EditableMovieList.ConvertChild(watchList.Movies);
            }

            MarkAsChild();
        }
    }
}
