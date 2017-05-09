using Csla;
using MovieBusinessLayer.BusinessModel.CriteriaObjects;
using MovieBusinessLayer.BusinessModel.Editables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel
{
    [Serializable]
    public class DirectorToMovie: BusinessBase<DirectorToMovie>
    {
        public static readonly PropertyInfo<EditableMovie> MovieProperty = RegisterProperty<EditableMovie>(c => c.Movie);
        public EditableMovie Movie
        {
            get { return GetProperty(MovieProperty); }
            set { SetProperty(MovieProperty, value); }
        }
        public static readonly PropertyInfo<int> MovieIdProperty = RegisterProperty<int>(c => c.MovieId);
        public int MovieId
        {
            get { return GetProperty(MovieIdProperty); }
            set { SetProperty(MovieIdProperty, value); }
        }
        public static readonly PropertyInfo<int> DirectorIdProperty = RegisterProperty<int>(c => c.DirectorId);
        public int DirectorId
        {
            get { return GetProperty(DirectorIdProperty); }
            set { SetProperty(DirectorIdProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(MovieIdProperty, "Please select a movie"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(DirectorIdProperty, "Please select a director"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinValue<int>(MovieIdProperty, 1,"Please select a movie"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinValue<int>(DirectorIdProperty, 1,"Please select a director"));
}

        public static DirectorToMovie AddDirectorToMovie(RelationsCriteria criteria)
        {
            return DataPortal.Create<DirectorToMovie>(criteria);
        }

        public static void DeleteDirectorFromMovie(int id)
        {
            DataPortal.Delete<DirectorToMovie>(id);
        }
        
        protected void DataPortal_Create(RelationsCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.MovieId = criteria.ParentId;
                this.DirectorId = criteria.ChildId;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                var movie = context.Movie.Find(this.MovieId);
                var director = context.Director.Find(this.DirectorId);
                if(movie == null)
                {
                    throw new Exception("There was no movie found");
                }
                if(director == null)
                {
                    throw new Exception("There was no director found");
                }
                movie.Director = director;
                context.SaveChanges();
                this.Movie = EditableMovie.GetMovieById(this.MovieId);
            }
        }

        private void DataPortal_Delete(int id)
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                var movie = context.Movie.Include("Director").Where(x => x.Id == id).FirstOrDefault();
                if (movie == null)
                {
                    throw new Exception("There was no movie found");
                }
                movie.Director = null;
                context.SaveChanges();
            }
        }
    }
}
