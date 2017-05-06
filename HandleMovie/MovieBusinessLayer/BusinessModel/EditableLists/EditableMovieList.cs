using Csla;
using MovieBusinessLayer.BusinessModel.Editables;
using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel.EditableLists
{
    [Serializable]
    public class EditableMovieList: BusinessListBase<EditableMovieList, EditableMovie>
    {
        public static EditableMovieList GetAllMovies()
        {
            return DataPortal.Fetch<EditableMovieList>();
        }

        public static EditableMovieList ConvertChild(List<Movie> movies)
        {
            return DataPortal.FetchChild<EditableMovieList>(movies);
        }

        private void DataPortal_Fetch()
        {
            List<Movie> movies = new List<Movie>();
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                movies = context.Movie.ToList();
            }
            if(movies.Count == 0)
            {
                throw new Exception("No movies were found");
            }
            foreach(var item in movies)
            {
                this.Add(EditableMovie.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<Movie> movies)
        {
            foreach(var item in movies)
            {
                this.Add(EditableMovie.ConvertChild(item));
            }
            MarkAsChild();
        }
    }
}
