using Csla;
using MovieBusinessLayer.BusinessModel.CriteriaObjects;
using MovieBusinessLayer.BusinessModel.EditableLists;
using MovieDataLayer.DataModels;
using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel.Editables
{
    [Serializable]
    public class EditableMovie: BusinessBase<EditableMovie>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }
        public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);
        public string Title
        {
            get { return GetProperty(TitleProperty); }
            set { SetProperty(TitleProperty, value); }
        }
        public static readonly PropertyInfo<string> CoverProperty = RegisterProperty<string>(c => c.Cover);
        public string Cover
        {
            get { return GetProperty(CoverProperty); }
            set { SetProperty(CoverProperty, value); }
        }
        public static readonly PropertyInfo<int> YearProperty = RegisterProperty<int>(c => c.Year);
        public int Year
        {
            get { return GetProperty(YearProperty); }
            set { SetProperty(YearProperty, value); }
        }
        public static readonly PropertyInfo<decimal> ScoreProperty = RegisterProperty<decimal>(c => c.Score);
        public decimal Score
        {
            get { return GetProperty(ScoreProperty); }
            set { SetProperty(ScoreProperty, value); }
        }
        public static readonly PropertyInfo<Genre> GenreProperty = RegisterProperty<Genre>(c => c.Genre);
        public Genre Genre
        {
            get { return GetProperty(GenreProperty); }
            set { SetProperty(GenreProperty, value); }
        }
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }
        public static readonly PropertyInfo<int> PlayTimeProperty = RegisterProperty<int>(c => c.PlayTime);
        public int PlayTime
        {
            get { return GetProperty(PlayTimeProperty); }
            set { SetProperty(PlayTimeProperty, value); }
        }
        public static readonly PropertyInfo<EditableDirector> DirectorProperty = RegisterProperty<EditableDirector>(c => c.Director);
        public EditableDirector Director
        {
            get { return GetProperty(DirectorProperty); }
            set { SetProperty(DirectorProperty, value); }
        }

        public static readonly PropertyInfo<EditableActorList> ActorsProperty = RegisterProperty<EditableActorList>(c => c.Actors);
        public EditableActorList Actors
        {
            get { return GetProperty(ActorsProperty); }
            set { SetProperty(ActorsProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(TitleProperty, "You need to add a title"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(YearProperty, "You need to add a year"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(ScoreProperty, "You need to add a score"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(DescriptionProperty, "You need to add a description"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(GenreProperty, "You need to choose a genre"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinValue<int>(PlayTimeProperty, 1, "Please give a valid number for the playing time"));
        }

        public static EditableMovie NewMovie(MovieCriteria criteria)
        {
            return DataPortal.Create<EditableMovie>(criteria);
        }

        public static void DeleteMovieById(int id)
        {
            DataPortal.Delete<EditableMovie>(id);
        }

        public static EditableMovie GetMovieById(int id)
        {
            return DataPortal.Fetch<EditableMovie>(id);
        }

        public static EditableMovie ConvertChild(Movie movie)
        {
            return DataPortal.FetchChild<EditableMovie>(movie);
        }

        protected void DataPortal_Create(MovieCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Title = criteria.Title;
                this.Year = criteria.Year;
                this.PlayTime = criteria.Playtime;
                this.Genre = criteria.Genre;
                this.Description = criteria.Description;
                this.Cover = criteria.Cover;
                this.Score = criteria.Score;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                Movie movie = new Movie() {
                    Cover = this.Cover,
                    Description = this.Description,
                    Genre = this.Genre,
                    Playtime = this.PlayTime,
                    Score = this.Score,
                    Title = this.Title,
                    Year = this.Year
                };
                context.Movie.Add(movie);
                context.SaveChanges();
                this.Id = movie.Id;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                var movie = context.Movie.Where(x => x.Id == this.Id).FirstOrDefault();
                movie.Cover = this.Cover;
                movie.Description = this.Description;
                movie.Genre = this.Genre;
                movie.Playtime = this.PlayTime;
                movie.Score = this.Score;
                movie.Title = this.Title;
                movie.Year = this.Year;
                context.SaveChanges();
            }
        }

        private void DataPortal_Delete(int id)
        {
            Movie movie;
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                movie = context.Movie.Include("Director").Include("Cast").Where(x => x.Id == id).FirstOrDefault();
                if (movie == null)
                    throw new Exception("No movie was found");
                if(movie.Cast != null && movie.Cast.Count > 0)
                {
                    List<Actor> actorsToRemove = new List<Actor>();
                    foreach(var item in movie.Cast)
                    {
                        if(item.CharacterName != null)
                        {
                            actorsToRemove.Add(item);
                        }
                    }
                    context.Actor.RemoveRange(actorsToRemove);
                }
                context.Movie.Remove(movie);
                context.SaveChanges();
            }
        }

        private void DataPortal_Fetch(int id)
        {
            Movie movie;
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                movie = context.Movie.Include("Director").Include("Cast").Where(x => x.Id == id).FirstOrDefault();
            }
            if(movie == null)
                throw new Exception("No movie was found");
            this.Id = movie.Id;
            this.Title = movie.Title;
            this.Year = movie.Year;
            this.PlayTime = movie.Playtime;
            this.Genre = movie.Genre;
            this.Description = movie.Description;
            this.Cover = movie.Cover;
            this.Score = movie.Score;
            if (movie.Director != null)
            {
                this.Director = EditableDirector.ConvertChild(movie.Director);
            }
            if (movie.Cast != null && movie.Cast.Count > 0)
            {
                List<Actor> actors = new List<Actor>();
                foreach(var item in movie.Cast)
                {
                    if (item.CharacterName != null)
                    {
                        actors.Add(item);
                    }
                }
                this.Actors = EditableActorList.ConvertChild(actors);
            }
        }

        private void Child_Fetch(Movie movie)
        {
            this.Id = movie.Id;
            this.Title = movie.Title;
            this.Year = movie.Year;
            this.PlayTime = movie.Playtime;
            this.Genre = movie.Genre;
            this.Cover = movie.Cover;
            this.Score = movie.Score;
            MarkAsChild();
        }
    }
}
