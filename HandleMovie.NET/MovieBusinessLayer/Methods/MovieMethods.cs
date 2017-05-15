using MovieBusinessLayer.BusinessLogicObjects;
using MovieDataLayer.DataModels;
using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace MovieBusinessLayer.Methods
{
    public static class MovieMethods
    {
        #region create  
        public static int CreateMovie(string title, string cover, int year, decimal score, Genre genre, string description, int playtime)
        {
            try
            {
                return BusinessMovie.CreateMovie(title, cover, year, score, genre, description, playtime);
            }
            catch(ValidationException ex)
            {
                throw;
            }
        }

        public static int CreateActor(string firstname, string familyname, string description, int countOscars, int age, Sex sex)
        {
            try
            {
                return BusinessActor.CreateActor(firstname,familyname,description,countOscars,age,sex);
            }
            catch (ValidationException ex)
            {
                throw;
            }
        }

        public static int CreateDirector(string firstname, string familyname, string description, int countOscars, int age, Sex sex)
        {
            try
            {
                return BusinessDirector.CreateDirector(firstname, familyname, description, countOscars, age, sex);
            }
            catch (ValidationException ex)
            {
                throw;
            }
        }

        public static int CreateWatchlist(string name)
        {
            try
            {
                return BusinessWatchlist.CreateWatchlist(name);
            }
            catch (ValidationException ex)
            {
                throw;
            }
        }
        #endregion

        #region read
        public static Actor GetActor(int id)
        {
            try
            {
                return BusinessActor.GetActor(id);
            }
            catch(ValidationException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static Director GetDirector(int id)
        {
            try
            {
                return BusinessDirector.GetDirector(id);
            }
            catch(ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Movie GetMovie(int id)
        {
            try
            {
                return BusinessMovie.GetMovie(id);
            }
            catch(ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static WatchList GetWatchlist(int id)
        {
            try
            {
                return BusinessWatchlist.GetWatchlist(id);
            }
            catch(ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<Actor> GetAllActors()
        {
            try
            {
                return BusinessActor.GetAllActors();
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<Director> GetAllDirectors()
        {
            try
            {
                return BusinessDirector.GetAllDirectors();
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<Movie> GetAllMovies()
        {
            try
            {
                return BusinessMovie.GetAllMovies();
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<WatchList> GetAllWatchlistsForUser(List<int> watchlists)
        {
            try
            {
                return BusinessWatchlist.GetAllWatchlistsForUser(watchlists);
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region delete

        public static bool DeleteMovie(int id)
        {
            try
            {
                BusinessMovie.DeleteMovie(id);
                return true;
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteActor(int id)
        {
            try
            {
                BusinessActor.DeleteActor(id);
                return true;
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteDirector(int id)
        {
            try
            {
                BusinessDirector.DeleteDirector(id);
                return true;
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteWatchlist(int id)
        {
            try
            {
                BusinessWatchlist.DeleteWatchlist(id);
                return true;
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region update
        public static Actor UpdateActor(int id, string firstname, string familyname, string description, int countOscars, int age, Sex sex)
        {
            try
            {
                return BusinessActor.UpdateActor(id, firstname, familyname, description, countOscars, age, sex);
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Director UpdateDirector(int id, string firstname, string familyname, string description, int countOscars, int age, Sex sex)
        {
            try
            {
                return BusinessDirector.UpdateDirector(id, firstname, familyname, description, countOscars, age, sex);
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Movie UpdateMovie(int id, string title, string cover, int year, decimal score, Genre genre, string description, int playtime)
        {
            try
            {
                return BusinessMovie.UpdateMovie(id, title, cover, year, score, genre, description, playtime);
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static WatchList UpdateWatchlist(int id, string name)
        {
            try
            {
                return BusinessWatchlist.UpdateWatchlist(id, name);
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region relations
        public static Movie AddActorToMovie(int movieId, int actorId, string charactername)
        {
            try
            {
                return BusinessMovie.AddActorToMovie(movieId, actorId, charactername);
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Movie AddDirectorToMovie(int movieId, int directorId)
        {
            try
            {
                return BusinessMovie.AddDirectorToMovie(movieId, directorId);
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static WatchList AddMovieToWatchlist(int watchlistId, int movieId)
        {
            try
            {
                return BusinessWatchlist.AddMovieToWatchlist(watchlistId, movieId);
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteActorFromMovie(int movieId, int actorId)
        {
            try
            {
                BusinessMovie.DeleteActorFromMovie(movieId, actorId);
                return true;
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteDirectorFromMovie(int movieId)
        {
            try
            {
                BusinessMovie.DeleteDirectorFromMovie(movieId);
                return true;
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteMovieFromWatchlist(int watchlistId, int movieId)
        {
            try
            {
                BusinessWatchlist.DeleteMovieFromWatchlist(watchlistId, movieId);
                return true;
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        public static void ValidateIncomingRequest(string application, string password)
        {
            if (string.IsNullOrWhiteSpace(application))
            {
                throw new Exception("No application entered");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password");
            }
            if (application != WebConfigurationManager.AppSettings["Application"])
            {
                throw new Exception("Wrong application");
            }
            if (password != WebConfigurationManager.AppSettings["Password"])
            {
                throw new Exception("Wrong password");
            }
        }
    }
}
