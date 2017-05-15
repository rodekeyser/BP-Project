using MovieDataLayer.DataModels;
using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessLogicObjects
{
    public class BusinessMovie
    {
        public static int CreateMovie(string title, string cover, int year, decimal score, Genre genre, string description, int playtime)
        {
            CheckMovieParameters(title, year, score, playtime, description);

            Movie movieToAdd = new Movie();
            movieToAdd.Title = title;
            movieToAdd.Cover = cover;
            movieToAdd.Year = year;
            movieToAdd.Score = score;
            movieToAdd.Genre = genre;
            movieToAdd.Description = description;
            movieToAdd.Playtime = playtime;
            using(var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                context.Movie.Add(movieToAdd);
                context.SaveChanges();
            }
            return movieToAdd.Id;
        }

        public static List<Movie> GetAllMovies()
        {
            List<Movie> movies;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                movies = context.Movie.ToList();
            }
            if (movies == null)
                throw new ValidationException("No movies were found", "None");
            return movies;
        }

        public static Movie GetMovie(int id)
        {
            Movie movie;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                movie = context.Movie.Include("Director").Include("Cast").Include("Watchlists").Where(x => x.Id == id).FirstOrDefault();
                if (movie == null)
                    throw new ValidationException("No movie was found", "Id");
            }
            return movie;
        }

        public static void DeleteMovie(int id)
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                var movieToDelete = context.Movie.Find(id);
                if (movieToDelete == null)
                    throw new ValidationException("No movie was found to delete", "Id");
                context.Movie.Remove(movieToDelete);
                context.SaveChanges();
            }
        }

        public static Movie UpdateMovie(int id, string title, string cover, int year, decimal score, Genre genre, string description, int playtime)
        {
            CheckMovieParameters(title, year, score, playtime, description);
            Movie movie;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                movie = context.Movie.Find(id);
                if (movie == null)
                    throw new ValidationException("No movie was found", "Id");
                movie.Title = title;
                movie.Cover = cover;
                movie.Description = description;
                movie.Year = year;
                movie.Score = score;
                movie.Genre = genre;
                movie.Playtime = playtime;
                context.SaveChanges();
            }
            return movie;
        }

        public static Movie AddActorToMovie(int movieId, int actorId, string charactername)
        {
            Movie movie;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                movie = context.Movie.Include("Cast").Where(x => x.Id == movieId).FirstOrDefault();
                var actor = context.Actor.Include("Movies").Where(x => x.Id == actorId).FirstOrDefault();
                if (movie == null)
                    throw new ValidationException("There was no movie found", "MovieId");
                if (actor == null)
                    throw new ValidationException("There was no actor found", "ActorId");
                if (actor.Movies == null)
                {
                    actor.Movies = new List<Movie>();
                }
                if (!actor.Movies.Contains(movie))
                {
                    actor.Movies.Add(movie);
                    context.SaveChanges();
                    Actor newActor = new Actor();
                    newActor = actor;
                    newActor.CharacterName = charactername;
                    context.Actor.Add(newActor);
                    if (movie.Cast == null)
                    {
                        movie.Cast = new List<Actor>();
                    }
                    movie.Cast.Add(newActor);
                    context.SaveChanges();
                }
                else
                {
                    throw new ValidationException("The movie already has this actor", "ActorId");
                }
            }
            movie = GetMovie(movieId);
            return movie;
        }

        public static Movie AddDirectorToMovie(int movieId, int directorId)
        {
            Movie movie;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                movie = context.Movie.Find(movieId);
                var director = context.Director.Find(directorId);
                if (movie == null)
                {
                    throw new Exception("There was no movie found");
                }
                if (director == null)
                {
                    throw new Exception("There was no director found");
                }
                movie.Director = director;
                context.SaveChanges();
            }
            return movie;
        }

        public static void DeleteActorFromMovie(int movieId, int actorId)
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                var movie = context.Movie.Include("Cast").Where(x => x.Id == movieId).FirstOrDefault();
                var actor = context.Actor.Include("Movies").Where(x => x.Id == actorId).FirstOrDefault();
                var actors = context.Actor.Where(x => x.CharacterName == null);
                if (movie == null)
                    throw new ValidationException("There was no movie found", "movieId");
                if (actor == null)
                    throw new ValidationException("There was no actor found", "actorId");
                if (movie.Cast.Contains(actor))
                {
                    List<Actor> actorToUpdate = new List<Actor>();
                    foreach (var item in actors)
                    {
                        if (item.Movies != null)
                        {
                            if (item.Movies.Contains(movie) && item.FirstName == actor.FirstName && item.FamilyName == actor.FamilyName)
                            {
                                actorToUpdate.Add(item);
                            }
                        }
                    }
                    actorToUpdate[0].Movies.Remove(movie);
                    context.SaveChanges();
                    context.Actor.Remove(actor);
                    context.SaveChanges();
                }
                else
                {
                    throw new ValidationException("This movie does not contain the selected actor", "actorId");
                }
            }
        }

        public static void DeleteDirectorFromMovie(int movieId)
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                var movie = context.Movie.Include("Director").Where(x => x.Id == movieId).FirstOrDefault();
                if (movie == null)
                {
                    throw new ValidationException("There was no movie found", "movieId");
                }
                movie.Director = null;
                context.SaveChanges();
            }
        }

        public static void CheckMovieParameters(string title, int year, decimal score, int playtime, string description)
        {
            if (title == null)
                throw new ValidationException("You need to add a title", "Title");

            if (year < 1900)
                throw new ValidationException("Please give a valid release year", "Year");

            if (score <= 0)
                throw new ValidationException("Please give a valid score", "Score");

            if (playtime < 1)
                throw new ValidationException("Please give a valid number for the playing time", "PlayTime");

            if (description == null)
                throw new ValidationException("You need to add a description", "Description");
        }
    }
}
