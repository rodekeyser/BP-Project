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
    public class DirectorToMovie: BusinessBase<DirectorToMovie>
    {
        public static readonly PropertyInfo<int> DirectorIdProperty = RegisterProperty<int>(c => c.DirectorId);
        public int DirectorId
        {
            get { return GetProperty(DirectorIdProperty); }
            set { SetProperty(DirectorIdProperty, value); }
        }
        public static readonly PropertyInfo<int> MovieIdProperty = RegisterProperty<int>(c => c.MovieId);
        public int MovieId
        {
            get { return GetProperty(MovieIdProperty); }
            set { SetProperty(MovieIdProperty, value); }
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

        public static DirectorToMovie AddDirectorToMovie(RelationsCriteria criteria)
        {
            return DataPortal.Create<DirectorToMovie>(criteria);
        }
        public static void RemoveDirectorFromMovie(int id)
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
            MovieResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.AddDirectorToMovie(new RelationInput()
                {
                    ChildId = this.DirectorId,
                    ParentId = this.MovieId,
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
                this.Movie = NamedMovie.GetMovieById(this.MovieId);
            }
        }

        private void DataPortal_Delete(int id)
        {
            BaseResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.DeleteDirectorFromMovie(new IdentificationInput()
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
    }
}
