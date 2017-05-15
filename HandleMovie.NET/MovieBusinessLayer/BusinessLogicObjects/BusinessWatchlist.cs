using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessLogicObjects
{
    public class BusinessWatchlist
    {
        public static int CreateWatchlist(string name)
        {
            CheckWatchlistParameters(name);
            WatchList wlToAdd = new WatchList();
            wlToAdd.Name = name;
            using(var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                context.Watchlist.Add(wlToAdd);
                context.SaveChanges();
            }
            return wlToAdd.Id;
        }

        public static List<WatchList> GetAllWatchlistsForUser(List<int> watchlists)
        {
            List<WatchList> wls = new List<WatchList>();
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                foreach(var item in watchlists)
                {
                    var wl = context.Watchlist.Find(item);
                    if (wl == null)
                        throw new ValidationException("No director found for id " + item.ToString(), "none");
                    wls.Add(wl);
                }
            }
            return wls;
        }

        public static WatchList GetWatchlist(int id)
        {
            WatchList wl;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                wl = context.Watchlist.Include("Movies").Where(x => x.Id == id).FirstOrDefault();
                if (wl == null)
                    throw new ValidationException("No watchlist was found", "Id");
            }
            return wl;
        }

        public static void DeleteWatchlist(int id)
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                var wlToDelete = context.Watchlist.Find(id);
                if (wlToDelete == null)
                    throw new ValidationException("No movie was found to delete", "Id");
                context.Watchlist.Remove(wlToDelete);
                context.SaveChanges();
            }
        }

        public static WatchList UpdateWatchlist(int id, string name)
        {
            CheckWatchlistParameters(name);
            WatchList watchlist;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                watchlist = context.Watchlist.Find(id);
                if(watchlist == null)
                    throw new ValidationException("No watchlist was found", "Id");
                watchlist.Name = name;
                context.SaveChanges();
            }
            return watchlist;
        }

        public static WatchList AddMovieToWatchlist(int watchlistId, int movieId)
        {
            WatchList watchlist;
            Movie movie;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                watchlist = context.Watchlist.Include("Movies").Where(x => x.Id == watchlistId).FirstOrDefault();
                movie = context.Movie.Find(movieId);
                if (watchlist == null)
                    throw new ValidationException("There was no watchlist found", "watchlistId");
                if (movie == null)
                    throw new ValidationException("There was no movie found", "movieId");
                if (!watchlist.Movies.Contains(movie))
                {
                    watchlist.Movies.Add(movie);
                    context.SaveChanges();
                }
                else
                    throw new ValidationException("The watchlist already contains this movie", "watchlistId");
            }
            return watchlist;
        }

        public static void DeleteMovieFromWatchlist(int watchlistId, int movieId)
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                var watchlist = context.Watchlist.Include("Movies").Where(x => x.Id == watchlistId).FirstOrDefault();
                var movie = context.Movie.Find(movieId);
                if (watchlist == null)
                    throw new ValidationException("There was no watchlist found", "watchlistId");
                if (movie == null)
                    throw new ValidationException("There was no movie found", "movieId");
                if (watchlist.Movies.Contains(movie))
                {
                    watchlist.Movies.Remove(movie);
                    context.SaveChanges();
                }
                else
                    throw new ValidationException("The watchlist does not contain this movie", "movieId");
            }
        }

        public static void CheckWatchlistParameters(string name)
        {
            if (name == null)
            {
                throw new ValidationException("You need to add a name.", "Name");
            }
        }
    }
}
