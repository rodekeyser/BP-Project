using MovieBusinessLayer.BusinessModel.CriteriaObjects;
using MovieBusinessLayer.UseCases;
using MovieCommunicationLayer.DataContracts.Input;
using MovieCommunicationLayer.DataContracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MovieCommunicationLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        #region create
        public IdentificationResponse CreateWatchlist(WatchlistInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newMovie = MovieUseCases.CreateWatchlist(new WatchlistCriteria() {
                    Name = input.Name
                });
                if (!newMovie.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in newMovie.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = newMovie.Id;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public IdentificationResponse CreateMovie(MovieInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newMovie = MovieUseCases.CreateMovie(new MovieCriteria() {
                    Cover = input.Cover,
                    Description = input.Description,
                    Genre = input.Genre,
                    Playtime  = input.Playtime,
                    Score = input.Score,
                    Title = input.Title,
                    Year = input.Year
                });
                if (!newMovie.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in newMovie.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = newMovie.Id;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public IdentificationResponse CreateActor(StakeholderInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newActor = MovieUseCases.CreateActor(new StakeholderCriteria()
                {
                    Age = input.Age,
                    CountOscars = input.CountOscars,
                    Description = input.Description,
                    FamilyName = input.FamilyName,
                    FirstName = input.FirstName,
                    Sex = input.Sex
                });
                if (!newActor.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in newActor.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = newActor.Id;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.Message
                });
                return response;
            }
        }

        public IdentificationResponse CreateDirector(StakeholderInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newDirector = MovieUseCases.CreateDirector(new StakeholderCriteria()
                {
                    Age = input.Age,
                    CountOscars = input.CountOscars,
                    Description = input.Description,
                    FamilyName = input.FamilyName,
                    FirstName = input.FirstName,
                    Sex = input.Sex
                });
                if (!newDirector.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in newDirector.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = newDirector.Id;
                }
                return response;
            }
            catch(Exception ex)
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

        public BaseResponse DeleteWatchlistById(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieUseCases.DeleteWatchlistById(input.Id);
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public BaseResponse DeleteMovieById(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieUseCases.DeleteMovieById(input.Id);
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public BaseResponse DeleteDirectorById(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieUseCases.DeleteDirectorById(input.Id);
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public BaseResponse DeleteActorById(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieUseCases.DeleteActorById(input.Id);
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }
        #endregion

        #region read

        public WatchlistResponse GetWatchlistById(IdentificationInput input)
        {
            WatchlistResponse response = new WatchlistResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var watchlist = MovieUseCases.GetWatchlistById(input.Id);
                if (watchlist != null)
                {
                    response.Name = watchlist.Name;
                    response.Id = watchlist.Id;
                    response.Succes = true;
                    if(watchlist.Movies != null)
                    {
                        foreach(var item in watchlist.Movies)
                        {
                            response.Movies.Add(new ChildMovieResponse()
                            {
                                Id = item.Id,
                                Cover = item.Cover,
                                Genre = item.Genre,
                                Playtime = item.PlayTime,
                                Title = item.Title,
                                Year = item.Year
                            });
                        }
                    }
                }
                else
                {
                    response.Succes = false;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public MovieResponse GetMovieById(IdentificationInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var movie = MovieUseCases.GetMovieById(input.Id);
                if(movie != null)
                {
                    response.Succes = true;
                    response.Id = movie.Id;
                    response.Playtime = movie.PlayTime;
                    response.Score = movie.Score;
                    response.Title = movie.Title;
                    response.Cover = movie.Cover;
                    response.Description = movie.Description;
                    response.Year = movie.Year;
                    response.Genre = movie.Genre;
                    if(movie.Director != null)
                    {
                        response.Director = new ChildStakeholderResponse() {
                            FamilyName = movie.Director.FamilyName,
                            FirstName = movie.Director.FirstName,
                            Id = movie.Director.Id
                        };
                    }
                    if (movie.Actors != null)
                    {
                        foreach (var item in movie.Actors)
                        {
                            response.Cast.Add(new ActorMovieResponse()
                            {
                                CharacterName = item.CharacterName,
                                FamilyName = item.FamilyName,
                                FirstName = item.FirstName,
                                Id = item.Id
                            });
                        }
                    }
                }
                else
                {
                    response.Succes = false;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;

            }
        }

        public StakeholderResponse GetDirectorById(IdentificationInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var director = MovieUseCases.GetDirectorById(input.Id);
                if(director!= null)
                {
                    response.Succes = true;
                    response.Sex = director.Sex;
                    response.Id = director.Id;
                    response.FirstName = director.FirstName;
                    response.FamilyName = director.FamilyName;
                    response.Description = director.Description;
                    response.CountOscars = director.CountOscars;
                    response.Age = director.Age;
                    if (director.Movies != null)
                    {
                        response.Succes = true;
                        foreach (var item in director.Movies)
                        {
                            response.Movies.Add(new ChildMovieResponse()
                            {
                                Cover = item.Cover,
                                Genre = item.Genre,
                                Id = item.Id,
                                Playtime = item.PlayTime,
                                Title = item.Title,
                                Year = item.Year
                            });
                        }
                    }
                }
                else
                {
                    response.Succes = false;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public StakeholderResponse GetActorById(IdentificationInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var actor = MovieUseCases.GetActorById(input.Id);
                if (actor != null)
                {
                    response.Succes = true;
                    response.Sex = actor.Sex;
                    response.Id = actor.Id;
                    response.FirstName = actor.FirstName;
                    response.FamilyName = actor.FamilyName;
                    response.Description = actor.Description;
                    response.CountOscars = actor.CountOscars;
                    response.Age = actor.Age;
                    if (actor.Movies != null)
                    {
                        response.Succes = true;
                        foreach (var item in actor.Movies)
                        {
                            response.Movies.Add(new ChildMovieResponse()
                            {
                                Cover = item.Cover,
                                Genre = item.Genre,
                                Id = item.Id,
                                Playtime = item.PlayTime,
                                Title = item.Title,
                                Year = item.Year
                            });
                        }
                    }
                }
                else
                {
                    response.Succes = false;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public WatchlistListResponse GetAllWatchlistsOfUser(MultipleIdentificationsInput input)
        {
            WatchlistListResponse response = new WatchlistListResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Application);
                var watchlists = MovieUseCases.GetAllWatchListsForUser(input.Ids);
                if(watchlists != null && watchlists.Count > 0) {
                    foreach (var item in watchlists)
                    {
                        WatchlistResponse watchlist = new WatchlistResponse()
                        {
                            Id = item.Id,
                            Name = item.Name
                        };
                        if (item.Movies != null)
                        {
                            response.Succes = true;
                            foreach (var itemm in item.Movies)
                            {
                                watchlist.Movies.Add(new ChildMovieResponse()
                                {
                                    Cover = itemm.Cover,
                                    Genre = itemm.Genre,
                                    Id = itemm.Id,
                                    Playtime = itemm.PlayTime,
                                    Title = itemm.Title,
                                    Year = itemm.Year
                                });
                            }
                        }
                        response.Watchlists.Add(watchlist);
                    }
                }
                else
                {
                    response.Succes = false;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public MovieListResponse GetAllMovies(BaseInput input)
        {
            MovieListResponse response = new MovieListResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var movies = MovieUseCases.GetAllMovies();
                if(movies != null && movies.Count > 0)
                {
                    response.Succes = true;
                    foreach(var item in movies)
                    {
                        response.Movies.Add(new ChildMovieResponse() {
                            Cover = item.Cover,
                            Genre = item.Genre,
                            Id = item.Id,
                            Playtime = item.PlayTime,
                            Title = item.Title,
                            Year = item.Year
                        });
                    }
                }
                else
                {
                    response.Succes = false;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public StakeholderListResponse GetAllDirectors(BaseInput input)
        {
            StakeholderListResponse response = new StakeholderListResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var directors = MovieUseCases.GetAllDirectors();
                if(directors != null && directors.Count > 0)
                {
                    response.Succes = true;
                    foreach(var item in directors)
                    {
                        response.Stakeholders.Add(new ChildStakeholderResponse()
                        {
                            FamilyName = item.FamilyName,
                            FirstName = item.FirstName,
                            Id = item.Id
                        });
                    }
                }
                else
                {
                    response.Succes = false;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;

            }
        }

        public StakeholderListResponse GetAllActors(BaseInput input)
        {
            StakeholderListResponse response = new StakeholderListResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var actors = MovieUseCases.GetAllActors();
                if (actors != null && actors.Count > 0)
                {
                    response.Succes = true;
                    foreach (var item in actors)
                    {
                        response.Stakeholders.Add(new ChildStakeholderResponse()
                        {
                            FamilyName = item.FamilyName,
                            FirstName = item.FirstName,
                            Id = item.Id
                        });
                    }
                }
                else
                {
                    response.Succes = false;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;

            }
        }
        #endregion

        #region addrelation
        public MovieResponse AddDirectorToMovie(RelationInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var relation = MovieUseCases.AddDirectorToMovie(new RelationsCriteria()
                {
                    ChildId = input.ChildId,
                    ParentId = input.ParentId
                });
                if (!relation.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in relation.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = relation.Movie.Id;
                    response.Playtime = relation.Movie.PlayTime;
                    response.Score = relation.Movie.Score;
                    response.Title = relation.Movie.Title;
                    response.Cover = relation.Movie.Cover;
                    response.Description = relation.Movie.Description;
                    response.Year = relation.Movie.Year;
                    response.Genre = relation.Movie.Genre;
                    if (relation.Movie.Director != null)
                    {
                        response.Director = new ChildStakeholderResponse()
                        {
                            FamilyName = relation.Movie.Director.FamilyName,
                            FirstName = relation.Movie.Director.FirstName,
                            Id = relation.Movie.Director.Id
                        };
                    }
                    if (relation.Movie.Actors != null)
                    {
                        foreach (var item in relation.Movie.Actors)
                        {
                            response.Cast.Add(new ActorMovieResponse()
                            {
                                CharacterName = item.CharacterName,
                                FamilyName = item.FamilyName,
                                FirstName = item.FirstName,
                                Id = item.Id
                            });
                        }
                    }
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public MovieResponse AddActorToMovie(RelationInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var relation = MovieUseCases.AddActorToMovie(new RelationsCriteria()
                {
                    ChildId = input.ChildId,
                    ParentId = input.ParentId,
                    CharacterName = input.CharacterName
                });
                if (!relation.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in relation.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = relation.Movie.Id;
                    response.Playtime = relation.Movie.PlayTime;
                    response.Score = relation.Movie.Score;
                    response.Title = relation.Movie.Title;
                    response.Cover = relation.Movie.Cover;
                    response.Description = relation.Movie.Description;
                    response.Year = relation.Movie.Year;
                    response.Genre = relation.Movie.Genre;
                    if (relation.Movie.Director != null)
                    {
                        response.Director = new ChildStakeholderResponse()
                        {
                            FamilyName = relation.Movie.Director.FamilyName,
                            FirstName = relation.Movie.Director.FirstName,
                            Id = relation.Movie.Director.Id
                        };
                    }
                    if (relation.Movie.Actors != null)
                    {
                        foreach (var item in relation.Movie.Actors)
                        {
                            response.Cast.Add(new ActorMovieResponse()
                            {
                                CharacterName = item.CharacterName,
                                FamilyName = item.FamilyName,
                                FirstName = item.FirstName,
                                Id = item.Id
                            });
                        }
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public WatchlistResponse AddMovieToWatchList(RelationInput input)
        {
            WatchlistResponse response = new WatchlistResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var relation = MovieUseCases.AddMovieToWatchlist(new RelationsCriteria()
                {
                    ParentId = input.ParentId,
                    ChildId = input.ChildId
                });
                if (!relation.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in relation.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Name = relation.Watchlist.Name;
                    response.Id = relation.Watchlist.Id;
                    response.Succes = true;
                    if (relation.Watchlist.Movies != null)
                    {
                        foreach (var item in relation.Watchlist.Movies)
                        {
                            response.Movies.Add(new ChildMovieResponse()
                            {
                                Id = item.Id,
                                Cover = item.Cover,
                                Genre = item.Genre,
                                Playtime = item.PlayTime,
                                Title = item.Title,
                                Year = item.Year
                            });
                        }
                    }
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public BaseResponse DeleteDirectorFromMovie(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieUseCases.DeleteDirectorFromMovie(input.Id);
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public BaseResponse DeleteActorFromMovie(RelationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieUseCases.DeleteActorFromMovie(new RelationsCriteria() {
                     ParentId = input.ParentId,
                     ChildId = input.ChildId
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }

        public BaseResponse DeleteMovieFromWatchlist(RelationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieUseCases.DeleteMovieFromWatchlist(new RelationsCriteria()
                {
                    ParentId = input.ParentId,
                    ChildId = input.ChildId
                });
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
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
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedActor = MovieUseCases.UpdateActor(new StakeholderCriteria()
                {
                    Age = input.Age,
                    CountOscars = input.CountOscars,
                    Description = input.Description,
                    FamilyName = input.FamilyName,
                    FirstName = input.FirstName,
                    Id = input.Id,
                    Sex = input.Sex
                });
                if (!updatedActor.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in updatedActor.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Age = updatedActor.Age;
                    response.CountOscars = updatedActor.CountOscars;
                    response.Description = updatedActor.Description;
                    response.FamilyName = updatedActor.FamilyName;
                    response.FirstName = updatedActor.FirstName;
                    response.Id = updatedActor.Id;
                    response.Sex = updatedActor.Sex;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }
        public StakeholderResponse UpdateDirector(StakeholderInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedDirector = MovieUseCases.UpdateDirector(new StakeholderCriteria()
                {
                    Age = input.Age,
                    CountOscars = input.CountOscars,
                    Description = input.Description,
                    FamilyName = input.FamilyName,
                    FirstName = input.FirstName,
                    Id = input.Id,
                    Sex = input.Sex
                });
                if (!updatedDirector.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in updatedDirector.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Age = updatedDirector.Age;
                    response.CountOscars = updatedDirector.CountOscars;
                    response.Description = updatedDirector.Description;
                    response.FamilyName = updatedDirector.FamilyName;
                    response.FirstName = updatedDirector.FirstName;
                    response.Id = updatedDirector.Id;
                    response.Sex = updatedDirector.Sex;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }
        public WatchlistResponse UpdateWatchlist(WatchlistInput input)
        {
            WatchlistResponse response = new WatchlistResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedWatchlist = MovieUseCases.UpdateWatchlist(new WatchlistCriteria() {
                    Id = input.Id,
                    Name = input.Name
                });
                if (!updatedWatchlist.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in updatedWatchlist.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Name = updatedWatchlist.Name;
                    response.Id = updatedWatchlist.Id;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }
        public MovieResponse UpdateMovie(MovieInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedMovie = MovieUseCases.UpdateMovie(new MovieCriteria() {
                    Cover = input.Cover,
                    Description = input.Description,
                    Genre = input.Genre,
                    Id = input.Id,
                    Playtime = input.Playtime,
                    Score = input.Score,
                    Title = input.Title,
                    Year = input.Year
                });
                if (!updatedMovie.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in updatedMovie.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = updatedMovie.Id;
                    response.Genre = updatedMovie.Genre;
                    response.Description = updatedMovie.Description;
                    response.Cover = updatedMovie.Cover;
                    response.Playtime = updatedMovie.PlayTime;
                    response.Score = updatedMovie.Score;
                    response.Title = updatedMovie.Title;
                    response.Year = updatedMovie.Year;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Errors.Add(new Error()
                {
                    ErrorMessage = ex.GetBaseException().Message
                });
                return response;
            }
        }
        #endregion
    }
}
