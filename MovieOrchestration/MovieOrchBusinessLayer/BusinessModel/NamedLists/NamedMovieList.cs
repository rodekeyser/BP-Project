using Csla;
using MovieOrchBusinessLayer.BusinessModel.NamedObjects;
using MovieOrchBusinessLayer.MovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.NamedLists
{
    [Serializable]
    public class NamedMovieList: BusinessListBase<NamedMovieList, NamedMovie>
    {
        public static NamedMovieList GetAllMovies()
        {
            return DataPortal.Fetch<NamedMovieList>();
        }

        public static NamedMovieList ConvertChild(List<ChildMovieResponse> movies)
        {
            return DataPortal.FetchChild<NamedMovieList>(movies);
        }

        private void DataPortal_Fetch()
        {
            MovieListResponse movies = new MovieListResponse();
            using (var service = new MovieServiceClient())
            {
                movies = service.GetAllMovies(new BaseInput() {
                    Application = "movie",
                    Password = "movie"
                });
            }
            if (!movies.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in movies.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
            foreach (var item in movies.Movies)
            {
                this.Add(NamedMovie.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<ChildMovieResponse> movies)
        {
            foreach (var item in movies)
            {
                this.Add(NamedMovie.ConvertChild(item));
            }
            MarkAsChild();
        }
    }
}
