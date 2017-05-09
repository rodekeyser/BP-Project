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
    public class ActorToMovie: BusinessBase<ActorToMovie>
    {
        public static readonly PropertyInfo<int> ActorIdProperty = RegisterProperty<int>(c => c.ActorId);
        public int ActorId
        {
            get { return GetProperty(ActorIdProperty); }
            set { SetProperty(ActorIdProperty, value); }
        }
        public static readonly PropertyInfo<int> MovieIdProperty = RegisterProperty<int>(c => c.MovieId);
        public int MovieId
        {
            get { return GetProperty(MovieIdProperty); }
            set { SetProperty(MovieIdProperty, value); }
        }
        public static readonly PropertyInfo<string> CharacterNameProperty = RegisterProperty<string>(c => c.CharacterName);
        public string CharacterName
        {
            get { return GetProperty(CharacterNameProperty); }
            set { SetProperty(CharacterNameProperty, value); }
        }
        public static readonly PropertyInfo<NamedMovie> MovieProperty = RegisterProperty<NamedMovie>(c => c.Movie);
        public NamedMovie Movie
        {
            get { return GetProperty(MovieProperty); }
            set { SetProperty(MovieProperty, value); }
        }

        public static readonly PropertyInfo<NamedExceptionReadOnlyList> ExceptionsProperty = RegisterProperty<NamedExceptionReadOnlyList>(c => c.Exceptions);
        public NamedExceptionReadOnlyList Exceptions
        {
            get { return GetProperty(ExceptionsProperty); }
            set { SetProperty(ExceptionsProperty, value); }
        }

        public static ActorToMovie AddActorToMovie(RelationsCriteria criteria)
        {
            return DataPortal.Create<ActorToMovie>(criteria);
        }

        public static void RemoveActorFromMovie(RelationsCriteria criteria)
        {
            DataPortal.Delete<ActorToMovie>(criteria);
        }

        protected void DataPortal_Create(RelationsCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.ActorId = criteria.ChildId;
                this.MovieId = criteria.ParentId;
                this.CharacterName = criteria.CharacterName;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            MovieResponse response;
            using (var service = new MovieServiceClient())
            {
                response = service.AddActorToMovie(new RelationInput()
                {
                    CharacterName = this.CharacterName,
                    ChildId = this.ActorId,
                    ParentId = this.MovieId,
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
                    this.Movie = NamedMovie.GetMovieById(this.MovieId);
                }
            }
        }

        private void DataPortal_Delete(RelationsCriteria criteria)
        {
            BaseResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.DeleteActorFromMovie(new RelationInput()
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
