using MovieBusinessLayer.BusinessModel;
using MovieBusinessLayer.BusinessModel.CriteriaObjects;
using MovieBusinessLayer.BusinessModel.EditableLists;
using MovieBusinessLayer.BusinessModel.Editables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace MovieBusinessLayer.UseCases
{
    public static class MovieUseCases
    {
        /* all getalls, getwatchlistofuser, alle getbyids, deletes, de creates, de addmovietowatchlist , adddirectortomovie, updates en addcasttomovie werken
          businessrules niet */
        #region read
        public static EditableWatchListList GetAllWatchListsForUser(List<int> watchlists)
        {
            try
            {
                return EditableWatchListList.GetAllWatchListsForPlayer(watchlists);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static EditableDirectorList GetAllDirectors()
        {
            try
            {
                return EditableDirectorList.GetAllDirectors();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static EditableMovieList GetAllMovies()
        {
            try
            {
                return EditableMovieList.GetAllMovies();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static EditableActorList GetAllActors()
        {
            try
            {
                return EditableActorList.GetAllActors();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        
        public static EditableActor GetActorById(int id)
        {
            try
            {
                return EditableActor.GetActorById(id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public static EditableDirector GetDirectorById(int id)
        {
            try
            {
                return EditableDirector.GetDirectorById(id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public static EditableMovie GetMovieById(int id)
        {
            try
            {
                return EditableMovie.GetMovieById(id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public static EditableWatchList GetWatchlistById(int id)
        {
            try
            {
                return EditableWatchList.GetWatchlistById(id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region delete
        public static bool DeleteActorById(int id)
        {
            try
            {
                EditableActor.DeleteActorById(id);
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public static bool DeleteDirectorById(int id)
        {
            try
            {
                EditableDirector.DeleteDirectorById(id);
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public static bool DeleteMovieById(int id)
        {
            try
            {
                EditableMovie.DeleteMovieById(id);
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public static bool DeleteWatchlistById(int id)
        {
            try
            {
                EditableWatchList.DeleteWatchlistById(id);
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region create
        public static EditableActor CreateActor(StakeholderCriteria criteria)
        {
            var newActor = EditableActor.NewActor(criteria);
            if (newActor.IsValid)
            {
                newActor = newActor.Save();
            }
            return newActor;
        }
        public static EditableDirector CreateDirector(StakeholderCriteria criteria)
        {
            var newDirector = EditableDirector.NewDirector(criteria);
            if (newDirector.IsValid)
            {
                newDirector = newDirector.Save();
            }
            return newDirector;
        }
        public static EditableMovie CreateMovie(MovieCriteria criteria)
        {
            var newMovie = EditableMovie.NewMovie(criteria);
            if (newMovie.IsValid)
            {
                newMovie = newMovie.Save();
            }
            return newMovie;
        }
        public static EditableWatchList CreateWatchlist(WatchlistCriteria criteria)
        {
            var newWatchlist = EditableWatchList.NewWatchlist(criteria);
            if (newWatchlist.IsValid)
            {
                newWatchlist = newWatchlist.Save();
            }
            return newWatchlist;
        }
        #endregion

        #region addrelation
        public static DirectorToMovie AddDirectorToMovie(RelationsCriteria criteria)
        {
            var movieWithChildren = DirectorToMovie.AddDirectorToMovie(criteria);
            if (movieWithChildren.IsValid)
            {
                movieWithChildren = movieWithChildren.Save();
            }
            return movieWithChildren;
        }

        public static ActorToMovie AddActorToMovie(RelationsCriteria criteria)
        {
            var movieWithChildren = ActorToMovie.AddActorToMovie(criteria);
            if (movieWithChildren.IsValid)
            {
                movieWithChildren = movieWithChildren.Save();
            }
            return movieWithChildren;
        }

        public static MovieToWatchlist AddMovieToWatchlist(RelationsCriteria criteria)
        {
            var movieWithChildren = MovieToWatchlist.AddMovieToWatchlist(criteria);
            if (movieWithChildren.IsValid)
            {
                movieWithChildren = movieWithChildren.Save();
            }
            return movieWithChildren;
        }

        public static bool DeleteDirectorFromMovie(int id)
        {
            try
            {
                DirectorToMovie.DeleteDirectorFromMovie(id);
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteActorFromMovie(RelationsCriteria criteria)
        {
            try
            {
                ActorToMovie.DeleteActorFromMovie(criteria);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteMovieFromWatchlist(RelationsCriteria criteria)
        {
            try
            {
                MovieToWatchlist.DeleteMovieFromWatchlist(criteria);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region update
        public static EditableActor UpdateActor(StakeholderCriteria criteria)
        {
            try
            {
                var actor = EditableActor.GetActorById(criteria.Id);
                actor.Age = criteria.Age;
                actor.CountOscars = criteria.CountOscars;
                actor.Description = criteria.Description;
                actor.FamilyName = criteria.FamilyName;
                actor.FirstName = criteria.FirstName;
                actor.Sex = criteria.Sex;
                if(actor.IsValid)
                actor = actor.Save();
                return actor;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static EditableDirector UpdateDirector(StakeholderCriteria criteria)
        {
            try
            {
                var director = EditableDirector.GetDirectorById(criteria.Id);
                director.Age = criteria.Age;
                director.CountOscars = criteria.CountOscars;
                director.Description = criteria.Description;
                director.FamilyName = criteria.FamilyName;
                director.FirstName = criteria.FirstName;
                director.Sex = criteria.Sex;
                if(director.IsValid)
                director = director.Save();
                return director;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static EditableWatchList UpdateWatchlist(WatchlistCriteria criteria)
        {
            try
            {
                var watchlist = EditableWatchList.GetWatchlistById(criteria.Id);
                watchlist.Name = criteria.Name;
                if(watchlist.IsValid)
                watchlist = watchlist.Save();
                return watchlist;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static EditableMovie UpdateMovie(MovieCriteria criteria)
        {
            try
            {
                var movie = EditableMovie.GetMovieById(criteria.Id);
                movie.Description = criteria.Description;
                movie.Cover = criteria.Cover;
                movie.Title = criteria.Title;
                movie.Score = criteria.Score;
                movie.Year = criteria.Year;
                movie.PlayTime = criteria.Playtime;
                movie.Genre = criteria.Genre;
                if(movie.IsValid)
                movie = movie.Save();
                return movie;
            }
            catch(Exception ex)
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
