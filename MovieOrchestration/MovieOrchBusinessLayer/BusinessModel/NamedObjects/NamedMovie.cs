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
    public class NamedMovie: BusinessBase<NamedMovie>
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
        public static readonly PropertyInfo<NamedDirector> DirectorProperty = RegisterProperty<NamedDirector>(c => c.Director);
        public NamedDirector Director
        {
            get { return GetProperty(DirectorProperty); }
            set { SetProperty(DirectorProperty, value); }
        }

        public static readonly PropertyInfo<NamedActorList> ActorsProperty = RegisterProperty<NamedActorList>(c => c.Actors);
        public NamedActorList Actors
        {
            get { return GetProperty(ActorsProperty); }
            set { SetProperty(ActorsProperty, value); }
        }
        public static readonly PropertyInfo<NamedExceptionReadOnlyList> ExceptionsProperty = RegisterProperty<NamedExceptionReadOnlyList>(c => c.Exceptions);
        public NamedExceptionReadOnlyList Exceptions
        {
            get { return GetProperty(ExceptionsProperty); }
            set { SetProperty(ExceptionsProperty, value); }
        }

        public static NamedMovie NewNamedMovie(MovieCriteria criteria)
        {
            return DataPortal.Create<NamedMovie>(criteria);
        }

        public static NamedMovie ConvertChild(ChildMovieResponse response)
        {
            return DataPortal.FetchChild<NamedMovie>(response);
        }

        public static NamedMovie GetMovieById(int id)
        {
            return DataPortal.Fetch<NamedMovie>(id);
        }

        public static void DeleteMovieById(int id)
        {
            DataPortal.Delete<NamedMovie>(id);
        }

        protected void DataPortal_Create(MovieCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Cover = criteria.Cover;
                this.Description = criteria.Description;
                this.Genre = criteria.Genre;
                this.PlayTime = criteria.Playtime;
                this.Score = criteria.Score;
                this.Title = criteria.Title;
                this.Year = criteria.Year;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            MovieInput movie = new MovieInput();
            movie.Cover = this.Cover;
            movie.Description = this.Description;
            movie.Genre = this.Genre;
            movie.Playtime = this.PlayTime;
            movie.Score = this.Score;
            movie.Title = this.Title;
            movie.Year = this.Year;
            movie.Application = "movie";
            movie.Password = "movie";
            IdentificationResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.CreateMovie(movie);
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
            MovieResponse response;
            using (var service = new MovieServiceClient())
            {
                response = service.GetMovieById(new IdentificationInput()
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
                this.Title = response.Title;
                this.Year = response.Year;
                this.PlayTime = response.Playtime;
                this.Genre = response.Genre;
                this.Description = response.Description;
                this.Cover = response.Cover;
                this.Score = response.Score;
                if (response.Director != null)
                {
                    this.Director = NamedDirector.ConvertChild(response.Director);
                }
                if (response.Cast != null && response.Cast.ToList().Count > 0)
                {
                    this.Actors = NamedActorList.ConvertChild(response.Cast.ToList());
                }
            }
        }

        private void Child_Fetch(ChildMovieResponse response)
        {
            this.Id = response.Id;
            this.Title = response.Title;
            this.Year = response.Year;
            this.PlayTime = response.Playtime;
            this.Genre = response.Genre;
            this.Cover = response.Cover;
            MarkAsChild();
        }

        private void DataPortal_Delete(int id)
        {
            BaseResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.DeleteMovieById(new IdentificationInput()
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
        }

        protected override void DataPortal_Update()
        {
            MovieResponse response;
            using (var service = new MovieServiceClient())
            {
                response = service.UpdateMovie(new MovieInput()
                {
                    Cover = this.Cover,
                    Description = this.Description,
                    Genre = this.Genre,
                    Id = this.Id,
                    Playtime = this.PlayTime,
                    Score = this.Score,
                    Title = this.Title,
                    Year = this.Year,
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
                this.Id = response.Id;
                this.Title = response.Title;
                this.Year = response.Year;
                this.PlayTime = response.Playtime;
                this.Genre = response.Genre;
                this.Description = response.Description;
                this.Cover = response.Cover;
                this.Score = response.Score;
            }
        }
    }
}
