using MovieOrchBusinessLayer.BusinessModel;
using MovieOrchBusinessLayer.BusinessModel.CriteriaObjects;
using MovieOrchBusinessLayer.BusinessModel.NamedLists;
using MovieOrchBusinessLayer.BusinessModel.NamedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace MovieOrchBusinessLayer.UseCases
{
    public static class MovieOrchUseCases
    {
        #region create
        public static NamedAchievement CreateAchievement(AchievementCriteria criteria)
        {
            var achievement = NamedAchievement.NewNamedAchievement(criteria);
            if (achievement.IsValid)
            {
                achievement = achievement.Save();
            }
            return achievement;
        }

        public static NamedActor CreateActor(StakeholderCriteria criteria)
        {
            var actor = NamedActor.NewNamedActor(criteria);
            if (actor.IsValid)
            {
                actor = actor.Save();
            }
            return actor;
        }

        public static NamedAdmin CreateAdmin(ClientCriteria criteria)
        {
            var admin = NamedAdmin.NewNamedAdmin(criteria);
            if (admin.IsValid)
            {
                admin = admin.Save();
            }
            return admin;
        }

        public static NamedDirector CreateDirector(StakeholderCriteria criteria)
        {
            var director = NamedDirector.NewNamedDirector(criteria);
            if (director.IsValid)
            {
                director = director.Save();
            }
            return director;
        }

        public static NamedMovie CreateMovie(MovieCriteria criteria)
        {
            var movie = NamedMovie.NewNamedMovie(criteria);
            if (movie.IsValid)
            {
                movie = movie.Save();
            }
            return movie;
        }

        public static NamedUser CreateUser(ClientCriteria criteria)
        {
            var user = NamedUser.NewNamedUser(criteria);
            if (user.IsValid)
            {
                user = user.Save();
            }
            return user;
        }

        public static NamedWatchlist CreateWatchlist(WatchlistCriteria criteria)
        {
            var wl = NamedWatchlist.NewNamedWatchlist(criteria);
            if (wl.IsValid)
            {
                wl = wl.Save();
            }
            return wl;
        }
        #endregion

        #region read
        public static NamedAchievementList GetAllAchievements()
        {
            try
            {
                return NamedAchievementList.GetAllAchievements();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static NamedActorList GetAllActors()
        {
            try
            {
                return NamedActorList.GetAllActors();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedAdminList GetAllAdmins()
        {
            try
            {
                return NamedAdminList.GetAllAdmins();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedDirectorList GetAllDirectors()
        {
            try
            {
                return NamedDirectorList.GetAllDirectors();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedMovieList GetAllMovies()
        {
            try
            {
                return NamedMovieList.GetAllMovies();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedUserList GetAllUsers()
        {
            try
            {
                return NamedUserList.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedWatchlistList GetAllWatchlistsOfUser(int userId)
        {
            try
            {
                return NamedWatchlistList.GetAllWatchListsForPlayer(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedAchievement GetAchievementById(int id)
        {
            try
            {
                return NamedAchievement.GetAchievementById(id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static NamedActor GetActorById(int id)
        {
            try
            {
                return NamedActor.GetActorById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedAdmin GetAdminById(int id)
        {
            try
            {
                return NamedAdmin.GetAdminById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedDirector GetDirectorById(int id)
        {
            try
            {
                return NamedDirector.GetDirectorById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedMovie GetMovieById(int id)
        {
            try
            {
                return NamedMovie.GetMovieById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedUser GetUserById(int id)
        {
            try
            {
                return NamedUser.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedWatchlist GetWatchlistById(int id)
        {
            try
            {
                return NamedWatchlist.GetWatchlistById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region delete
        public static bool DeleteAchievement(int id)
        {
            try
            {
                NamedAchievement.DeleteAchievementById(id);
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteActor(int id)
        {
            try
            {
                NamedActor.DeleteActorById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteAdmin(int id)
        {
            try
            {
                NamedAdmin.DeleteAdminById(id);
                return true;
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
                NamedDirector.DeleteDirectorById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteMovie(int id)
        {
            try
            {
                NamedMovie.DeleteMovieById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteUser(int id)
        {
            try
            {
                NamedUser.DeleteUserById(id);
                return true;
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
                NamedWatchlist.DeleteWatchlistById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region update
        public static NamedAchievement UpdateAchievement(AchievementCriteria criteria)
        {
            try
            {
                var achiev = NamedAchievement.GetAchievementById(criteria.Id);
                achiev.Description = criteria.Description;
                achiev.Name = criteria.Name;
                if (achiev.IsValid)
                {
                    achiev = achiev.Save();
                }
                return achiev;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedActor UpdateActor(StakeholderCriteria criteria)
        {
            try
            {
                var actor = NamedActor.GetActorById(criteria.Id);
                actor.Age = criteria.Age;
                actor.CountOscars = criteria.CountOscars;
                actor.Description = criteria.Description;
                actor.FamilyName = criteria.FamilyName;
                actor.FirstName = criteria.FirstName;
                actor.Sex = criteria.Sex;
                if (actor.IsValid)
                    actor = actor.Save();
                return actor;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedAdmin UpdateAdmin(ClientCriteria criteria)
        {
            try
            {
                var admin = NamedAdmin.GetAdminById(criteria.Id);
                admin.Age = criteria.Age;
                admin.Email = criteria.Email;
                admin.Password = criteria.Password;
                admin.UserName = criteria.UserName;
                if (admin.IsValid)
                {
                    admin = admin.Save();
                }
                return admin;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedDirector UpdateDirector(StakeholderCriteria criteria)
        {
            try
            {
                var director = NamedDirector.GetDirectorById(criteria.Id);
                director.Age = criteria.Age;
                director.CountOscars = criteria.CountOscars;
                director.Description = criteria.Description;
                director.FamilyName = criteria.FamilyName;
                director.FirstName = criteria.FirstName;
                director.Sex = criteria.Sex;
                if (director.IsValid)
                    director = director.Save();
                return director;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedMovie UpdateMovie(MovieCriteria criteria)
        {
            try
            {
                var movie = NamedMovie.GetMovieById(criteria.Id);
                movie.Description = criteria.Description;
                movie.Cover = criteria.Cover;
                movie.Title = criteria.Title;
                movie.Score = criteria.Score;
                movie.Year = criteria.Year;
                movie.PlayTime = criteria.Playtime;
                movie.Genre = criteria.Genre;
                if (movie.IsValid)
                    movie = movie.Save();
                return movie;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedUser UpdateUser(ClientCriteria criteria)
        {
            try
            {
                var user = NamedUser.GetUserById(criteria.Id);
                user.Age = criteria.Age;
                user.Email = criteria.Email;
                user.Password = criteria.Password;
                user.UserName = criteria.UserName;
                if (user.IsValid)
                {
                    user = user.Save();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static NamedWatchlist UpdateWatchlist(WatchlistCriteria criteria)
        {
            try
            {
                var watchlist = NamedWatchlist.GetWatchlistById(criteria.Id);
                watchlist.Name = criteria.Name;
                if (watchlist.IsValid)
                    watchlist = watchlist.Save();
                return watchlist;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region relations
        public static AchievementToUser AddAchievementToUser(RelationsCriteria criteria)
        {
            try
            {
                var userWithChildren = AchievementToUser.AddAchievementToUser(criteria);
                if (userWithChildren.IsValid)
                {
                    userWithChildren = userWithChildren.Save();
                }
                return userWithChildren;
            }
            catch (Exception ex)
            {
                throw;
            }
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

        public static DirectorToMovie AddDirectorToMovie(RelationsCriteria criteria)
        {
            var movieWithChildren = DirectorToMovie.AddDirectorToMovie(criteria);
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

        public static UserToUser AddFriend(RelationsCriteria criteria)
        {
            try
            {
                var userWithFriends = UserToUser.AddFriend(criteria);
                if (userWithFriends.IsValid)
                {
                    userWithFriends = userWithFriends.Save();
                }
                return userWithFriends;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool RemoveDirectorFromMovie(int id)
        {
            try
            {
                DirectorToMovie.RemoveDirectorFromMovie(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool RemoveActorFromMovie(RelationsCriteria criteria)
        {
            try
            {
                ActorToMovie.RemoveActorFromMovie(criteria);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool RemoveMovieFromWatchlist(RelationsCriteria criteria)
        {
            try
            {
                MovieToWatchlist.RemoveMovieFromWatchlist(criteria);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool RemoveFriend(RelationsCriteria criteria)
        {
            try
            {
                UserToUser.RemoveFriend(criteria);
                return true;
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
                throw new Exception("No application entered");
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password");
            if (application != WebConfigurationManager.AppSettings["Application"])
                throw new Exception("Wrong application");
            if (password != WebConfigurationManager.AppSettings["Password"])
                throw new Exception("Wrong password");
        }
    }
}
