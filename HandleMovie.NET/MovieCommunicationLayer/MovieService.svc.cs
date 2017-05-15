using MovieBusinessLayer.BusinessLogicObjects;
using MovieBusinessLayer.Methods;
using MovieCommunicationLayer.DataContracts.Input;
using MovieCommunicationLayer.DataContracts.Response;
using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MovieCommunicationLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MovieService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MovieService.svc or MovieService.svc.cs at the Solution Explorer and start debugging.
    public class MovieService : IMovieService
    {
        #region create

        public IdentificationResponse CreateMovie(MovieInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                int index = MovieMethods.CreateMovie(input.Title, input.Cover, input.Year, input.Score, input.Genre, input.Description, input.Playtime);
                response.Succes = true;
                response.Id = index;
                return response;
            }
            catch(ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
        }

        public IdentificationResponse CreateActor(StakeholderInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                int index = MovieMethods.CreateActor(input.FirstName, input.FamilyName, input.Description, input.CountOscars, input.Age,input.Sex);
                response.Succes = true;
                response.Id = index;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
        }

        public IdentificationResponse CreateDirector(StakeholderInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                int index = MovieMethods.CreateDirector(input.FirstName, input.FamilyName, input.Description, input.CountOscars, input.Age, input.Sex);
                response.Succes = true;
                response.Id = index;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
        }

        public IdentificationResponse CreateWatchlist(WatchlistInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                int index = MovieMethods.CreateWatchlist(input.Name);
                response.Succes = true;
                response.Id = index;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
        }
        #endregion

        #region read
        public StakeholderResponse GetActor(IdentificationInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var actor = MovieMethods.GetActor(input.Id);
                response.Succes = true;
                response.Id = actor.Id;
                response.Age = actor.Age;
                response.CountOscars = actor.CountOscars;
                response.Description = actor.Description;
                response.FamilyName = actor.FamilyName;
                response.FirstName = actor.FirstName;
                response.Sex = actor.Sex;
                if(actor.Movies != null)
                {
                    foreach(var item in actor.Movies)
                    {
                        response.Movies.Add(new ChildMovieResponse()
                        {
                            Id = item.Id,
                            Cover = item.Cover,
                            Genre = item.Genre,
                            Playtime = item.Playtime,
                            Title = item.Title,
                            Year = item.Year
                        });
                    }
                }
                return response;
            }
            catch(ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public StakeholderResponse GetDirector(IdentificationInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var director = MovieMethods.GetDirector(input.Id);
                response.Succes = true;
                response.Id = director.Id;
                response.Age = director.Age;
                response.CountOscars = director.CountOscars;
                response.Description = director.Description;
                response.FamilyName = director.FamilyName;
                response.FirstName = director.FirstName;
                response.Sex = director.Sex;
                if (director.Movies != null)
                {
                    foreach (var item in director.Movies)
                    {
                        response.Movies.Add(new ChildMovieResponse()
                        {
                            Id = item.Id,
                            Cover = item.Cover,
                            Genre = item.Genre,
                            Playtime = item.Playtime,
                            Title = item.Title,
                            Year = item.Year
                        });
                    }
                }
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public MovieResponse GetMovie(IdentificationInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var movie = MovieMethods.GetMovie(input.Id);
                response.Succes = true;
                response.Id = movie.Id;
                response.Cover = movie.Cover;
                response.Genre = movie.Genre;
                response.Description = movie.Description;
                response.Playtime = movie.Playtime;
                response.Score = movie.Score;
                response.Title = movie.Title;
                response.Year = movie.Year;
                if (movie.Cast != null)
                {
                    List<Actor> actors = new List<Actor>();
                    foreach (var item in movie.Cast)
                    {
                        if (item.CharacterName != null)
                        {
                            actors.Add(item);
                        }
                    }
                    foreach (var item in actors)
                    {
                        response.Cast.Add(new ChildStakeholderResponse()
                        {
                            Id = item.Id,
                            CharacterName = item.CharacterName,
                            FamilyName = item.FamilyName,
                            FirstName = item.FirstName
                        });
                    }
                }
                if(movie.Director != null)
                {
                    response.Director = new ChildStakeholderResponse()
                    {
                        Id = movie.Director.Id,
                        FamilyName = movie.Director.FamilyName,
                        FirstName = movie.Director.FirstName
                    };
                }
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public WatchlistResponse GetWatchlist(IdentificationInput input)
        {
            WatchlistResponse response = new WatchlistResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var watchlist = MovieMethods.GetWatchlist(input.Id);
                response.Succes = true;
                response.Id = watchlist.Id;
                response.Name = watchlist.Name;
                if (watchlist.Movies != null)
                {
                    foreach (var item in watchlist.Movies)
                    {
                        response.Movies.Add(new ChildMovieResponse()
                        {
                            Id = item.Id,
                            Cover = item.Cover,
                            Genre = item.Genre,
                            Playtime = item.Playtime,
                            Title = item.Title,
                            Year = item.Year
                        });
                    }
                }
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public StakeholderListResponse GetAllActors(BaseInput input)
        {
            StakeholderListResponse response = new StakeholderListResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var actors = MovieMethods.GetAllActors();
                foreach(var item in actors)
                {
                    response.Stakeholders.Add(new ChildStakeholderResponse()
                    {
                        FamilyName = item.FamilyName,
                        FirstName = item.FirstName,
                        Id = item.Id
                    });
                }
                response.Succes = true;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public StakeholderListResponse GetAllDirectors(BaseInput input)
        {
            StakeholderListResponse response = new StakeholderListResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var directors = MovieMethods.GetAllDirectors();
                foreach (var item in directors)
                {
                    response.Stakeholders.Add(new ChildStakeholderResponse()
                    {
                        FamilyName = item.FamilyName,
                        FirstName = item.FirstName,
                        Id = item.Id
                    });
                }
                response.Succes = true;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public MovieListResponse GetAllMovies(BaseInput input)
        {
            MovieListResponse response = new MovieListResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var movies = MovieMethods.GetAllMovies();
                foreach(var item in movies)
                {
                    response.Movies.Add(new ChildMovieResponse()
                    {
                        Cover = item.Cover,
                        Genre = item.Genre,
                        Id = item.Id,
                        Playtime = item.Playtime,
                        Title = item.Title,
                        Year = item.Year
                    });
                }
                response.Succes = true;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public WatchlistListResponse GetAllWatchlistsForUser (MultipleIdentificationsInput input)
        {
            WatchlistListResponse response = new WatchlistListResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var wls = MovieMethods.GetAllWatchlistsForUser(input.Ids);
                foreach(var item in wls)
                {
                    List<ChildMovieResponse> movies = new List<ChildMovieResponse>();
                    if(item.Movies != null)
                    {
                        foreach(var itemm in item.Movies)
                        {
                            movies.Add(new ChildMovieResponse()
                            {
                                Cover = itemm.Cover,
                                Genre = itemm.Genre,
                                Id = itemm.Id,
                                Playtime = itemm.Playtime,
                                Title = itemm.Title,
                                Year = itemm.Year
                            });
                        }
                    }
                    response.Watchlists.Add(new WatchlistResponse()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Movies = movies
                    });
                }
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }
        #endregion

        #region delete
        public BaseResponse DeleteActor(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieMethods.DeleteActor(input.Id);
                return response;
            }
            catch(ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public BaseResponse DeleteDirector(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieMethods.DeleteDirector(input.Id);
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public BaseResponse DeleteMovie(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieMethods.DeleteMovie(input.Id);
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public BaseResponse DeleteWatchlist(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieMethods.DeleteWatchlist(input.Id);
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }
        #endregion

        #region update
        public StakeholderResponse UpdateActor(StakeholderInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var actor = MovieMethods.UpdateActor(input.Id, input.FirstName, input.FamilyName, input.Description, input.CountOscars, input.Age, input.Sex);
                response.Succes = true;
                response.Id = actor.Id;
                response.Age = actor.Age;
                response.CountOscars = actor.CountOscars;
                response.Description = actor.Description;
                response.FamilyName = actor.FamilyName;
                response.FirstName = actor.FirstName;
                response.Sex = actor.Sex;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public StakeholderResponse UpdateDirector(StakeholderInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var director = MovieMethods.UpdateDirector(input.Id, input.FirstName, input.FamilyName, input.Description, input.CountOscars, input.Age, input.Sex);
                response.Succes = true;
                response.Id = director.Id;
                response.Age = director.Age;
                response.CountOscars = director.CountOscars;
                response.Description = director.Description;
                response.FamilyName = director.FamilyName;
                response.FirstName = director.FirstName;
                response.Sex = director.Sex;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public MovieResponse UpdateMovie(MovieInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var movie = MovieMethods.UpdateMovie(input.Id, input.Title, input.Cover, input.Year, input.Score, input.Genre, input.Description, input.Playtime);
                response.Succes = true;
                response.Id = movie.Id;
                response.Cover = movie.Cover;
                response.Genre = movie.Genre;
                response.Description = movie.Description;
                response.Playtime = movie.Playtime;
                response.Score = movie.Score;
                response.Title = movie.Title;
                response.Year = movie.Year;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public WatchlistResponse UpdateWatchlist(WatchlistInput input)
        {
            WatchlistResponse response = new WatchlistResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var watchlist = MovieMethods.UpdateWatchlist(input.Id, input.Name);
                response.Name = watchlist.Name;
                response.Id = watchlist.Id;
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }
        #endregion

        #region relations
        public MovieResponse AddActorToMovie(RelationInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var movie = MovieMethods.AddActorToMovie(input.ParentId, input.ChildId, input.CharacterName);
                response.Succes = true;
                response.Id = movie.Id;
                response.Cover = movie.Cover;
                response.Genre = movie.Genre;
                response.Description = movie.Description;
                response.Playtime = movie.Playtime;
                response.Score = movie.Score;
                response.Title = movie.Title;
                response.Year = movie.Year;
                if (movie.Cast != null)
                {
                    List<Actor> actors = new List<Actor>();
                    foreach (var item in movie.Cast)
                    {
                        if (item.CharacterName != null)
                        {
                            actors.Add(item);
                        }
                    }
                    foreach (var item in actors)
                    {
                        response.Cast.Add(new ChildStakeholderResponse()
                        {
                            Id = item.Id,
                            CharacterName = item.CharacterName,
                            FamilyName = item.FamilyName,
                            FirstName = item.FirstName
                        });
                    }
                }
                if (movie.Director != null)
                {
                    response.Director = new ChildStakeholderResponse()
                    {
                        Id = movie.Director.Id,
                        FamilyName = movie.Director.FamilyName,
                        FirstName = movie.Director.FirstName
                    };
                }
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public MovieResponse AddDirectorMovie(RelationInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var movie = MovieMethods.AddDirectorToMovie(input.ParentId, input.ChildId);
                response.Succes = true;
                response.Id = movie.Id;
                response.Cover = movie.Cover;
                response.Genre = movie.Genre;
                response.Description = movie.Description;
                response.Playtime = movie.Playtime;
                response.Score = movie.Score;
                response.Title = movie.Title;
                response.Year = movie.Year;
                if (movie.Cast != null)
                {
                    foreach (var item in movie.Cast)
                    {
                        response.Cast.Add(new ChildStakeholderResponse()
                        {
                            Id = item.Id,
                            CharacterName = item.CharacterName,
                            FamilyName = item.FamilyName,
                            FirstName = item.FirstName
                        });
                    }
                }
                if (movie.Director != null)
                {
                    response.Director = new ChildStakeholderResponse()
                    {
                        Id = movie.Director.Id,
                        FamilyName = movie.Director.FamilyName,
                        FirstName = movie.Director.FirstName
                    };
                }
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public WatchlistResponse AddMovieToWatchlist(RelationInput input)
        {
            WatchlistResponse response = new WatchlistResponse();
            try
            {
                MovieMethods.ValidateIncomingRequest(input.Application, input.Password);
                var watchlist = MovieMethods.AddMovieToWatchlist(input.ParentId, input.ChildId);
                response.Succes = true;
                response.Id = watchlist.Id;
                response.Name = watchlist.Name;
                if (watchlist.Movies != null)
                {
                    foreach (var item in watchlist.Movies)
                    {
                        response.Movies.Add(new ChildMovieResponse()
                        {
                            Id = item.Id,
                            Cover = item.Cover,
                            Genre = item.Genre,
                            Playtime = item.Playtime,
                            Title = item.Title,
                            Year = item.Year
                        });
                    }
                }
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public BaseResponse DeleteActorFromMovie(RelationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response.Succes = MovieMethods.DeleteActorFromMovie(input.ParentId, input.ChildId);
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public BaseResponse DeleteDirectorFromMovie(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response.Succes = MovieMethods.DeleteDirectorFromMovie(input.Id);
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public BaseResponse DeleteMovieFromWatchlist(RelationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response.Succes = MovieMethods.DeleteMovieFromWatchlist(input.ParentId, input.ChildId);
                return response;
            }
            catch (ValidationException ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message,
                    PropertyName = ex.PropertyName
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }
        #endregion
    }
}
