using MovieOrchBusinessLayer.BusinessModel.NamedObjects;
using MovieOrchCommunicationLayer.DataContracts.Response;
using MovieOrchCommunicationLayer.DataContracts.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MovieOrchBusinessLayer.UseCases;
using MovieOrchBusinessLayer.BusinessModel.CriteriaObjects;

namespace MovieOrchCommunicationLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        #region create
        public IdentificationResponse CreateAchievement(AchievementInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newAchievement = MovieOrchUseCases.CreateAchievement(new AchievementCriteria()
                {
                    Description = input.Description,
                    Name = input.Name
                });
                if (newAchievement.Exceptions != null && newAchievement.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in newAchievement.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = newAchievement.Id;
                }
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

        public IdentificationResponse CreateActor(StakeholderInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newActor = MovieOrchUseCases.CreateActor(new StakeholderCriteria()
                {
                    Age = input.Age,
                    CountOscars = input.CountOscars,
                    Description = input.Description,
                    FamilyName = input.FamilyName,
                    FirstName = input.FirstName,
                    Sex = input.Sex
                });
                if (newActor.Exceptions != null && newActor.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in newActor.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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

        public IdentificationResponse CreateAdmin(ClientInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newAdmin = MovieOrchUseCases.CreateAdmin(new ClientCriteria()
                {
                    Age = input.Age,
                    Email = input.Email,
                    Password = input.ClientPassword,
                    UserName = input.UserName
                });
                if (newAdmin.Exceptions != null && newAdmin.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in newAdmin.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = newAdmin.Id;
                }
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

        public IdentificationResponse CreateDirector(StakeholderInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newDirector = MovieOrchUseCases.CreateDirector(new StakeholderCriteria()
                {
                    Age = input.Age,
                    CountOscars = input.CountOscars,
                    Description = input.Description,
                    FamilyName = input.FamilyName,
                    FirstName = input.FirstName,
                    Sex = input.Sex
                });
                if (newDirector.Exceptions != null && newDirector.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in newDirector.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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

        public IdentificationResponse CreateMovie(MovieInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newMovie = MovieOrchUseCases.CreateMovie(new MovieCriteria()
                {
                    Cover = input.Cover,
                    Description = input.Description,
                    Genre = input.Genre,
                    Playtime = input.Playtime,
                    Score = input.Score,
                    Title = input.Title,
                    Year = input.Year
                });
                if (newMovie.Exceptions != null && newMovie.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in newMovie.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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

        public IdentificationResponse CreateUser(ClientInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newUser = MovieOrchUseCases.CreateUser(new ClientCriteria()
                {
                    Age = input.Age,
                    Email = input.Email,
                    Password = input.ClientPassword,
                    UserName = input.UserName
                });
                if (newUser.Exceptions != null && newUser.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in newUser.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = newUser.Id;
                }
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

        public IdentificationResponse CreateWatchlist(WatchlistInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newMovie = MovieOrchUseCases.CreateWatchlist(new WatchlistCriteria()
                {
                    Name = input.Name,
                    UserId = input.UserId
                });
                if (newMovie.Exceptions != null && newMovie.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in newMovie.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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

        #region read
        public AchievementListResponse GetAllAchievements(BaseInput input)
        {
            AchievementListResponse response = new AchievementListResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var achievs = MovieOrchUseCases.GetAllAchievements();
                if (achievs != null && achievs.Count > 0)
                {
                    response.Succes = true; 
                    foreach (var item in achievs)
                    {
                        response.Achievements.Add(new AchievementResponse()
                        {
                            Id = item.Id,
                            Name = item.Name
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
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var actors = MovieOrchUseCases.GetAllActors();
                if (actors != null && actors.Count > 0)
                {
                    response.Succes = true;
                    foreach (var item in actors)
                    {
                        response.Stakeholders.Add(new StakeholderResponse()
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

        public ClientListResponse GetAllAdmins(BaseInput input)
        {
            ClientListResponse response = new ClientListResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var admins = MovieOrchUseCases.GetAllAdmins();
                if (admins != null)
                {
                    response.Succes = true;
                    foreach (var item in admins)
                    {
                        response.Clients.Add(new ClientResponse()
                        {
                            Id = item.Id,
                            UserName = item.UserName
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

        public StakeholderListResponse GetAllDirectors(BaseInput input)
        {
            StakeholderListResponse response = new StakeholderListResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var directors = MovieOrchUseCases.GetAllDirectors();
                if (directors != null && directors.Count > 0)
                {
                    response.Succes = true;
                    foreach (var item in directors)
                    {
                        response.Stakeholders.Add(new StakeholderResponse()
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

        public MovieListResponse GetAllMovies(BaseInput input)
        {
            MovieListResponse response = new MovieListResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var movies = MovieOrchUseCases.GetAllMovies();
                if (movies != null && movies.Count > 0)
                {
                    response.Succes = true;
                    foreach (var item in movies)
                    {
                        response.Movies.Add(new MovieResponse()
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

        public ClientListResponse GetAllUsers(BaseInput input)
        {
            ClientListResponse response = new ClientListResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var users = MovieOrchUseCases.GetAllUsers();
                if (users != null)
                {
                    response.Succes = true;
                    foreach (var item in users)
                    {
                        response.Clients.Add(new ClientResponse()
                        {
                            Id = item.Id,
                            UserName = item.UserName
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

        public WatchlistListResponse GetAllWatchlistsOfUser(IdentificationInput input)
        {
            WatchlistListResponse response = new WatchlistListResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var watchlists = MovieOrchUseCases.GetAllWatchlistsOfUser(input.Id);
                if (watchlists != null && watchlists.Count > 0)
                {
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
                                watchlist.Movies.Add(new MovieResponse()
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

        public AchievementResponse GetAchievementById(IdentificationInput input)
        {
            AchievementResponse response = new AchievementResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var achievement = MovieOrchUseCases.GetAchievementById(input.Id);
                if (achievement != null)
                {
                    response.Succes = true;
                    response.Description = achievement.Description;
                    response.Id = achievement.Id;
                    response.Name = achievement.Name;
                    if (achievement.Users != null)
                    {
                        foreach (var item in achievement.Users)
                        {
                            response.Users.Add(new ClientResponse()
                            {
                                UserName = item.UserName,
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

        public StakeholderResponse GetActorById(IdentificationInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var actor = MovieOrchUseCases.GetActorById(input.Id);
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
                            response.Movies.Add(new MovieResponse()
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

        public ClientResponse GetAdminById(IdentificationInput input)
        {
            ClientResponse response = new ClientResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var admin = MovieOrchUseCases.GetAdminById(input.Id);
                if (admin != null)
                {
                    response.Succes = true;
                    response.Id = admin.Id;
                    response.Email = admin.Email;
                    response.UserName = admin.UserName;
                    response.Password = admin.Password;
                    response.Age = admin.Age;
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

        public StakeholderResponse GetDirectorById(IdentificationInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var director = MovieOrchUseCases.GetDirectorById(input.Id);
                if (director != null)
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
                            response.Movies.Add(new MovieResponse()
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

        public MovieResponse GetMovieById(IdentificationInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var movie = MovieOrchUseCases.GetMovieById(input.Id);
                if (movie != null)
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
                    if (movie.Director != null)
                    {
                        response.Director = new StakeholderResponse()
                        {
                            FamilyName = movie.Director.FamilyName,
                            FirstName = movie.Director.FirstName,
                            Id = movie.Director.Id
                        };
                    }
                    if (movie.Actors != null)
                    {
                        foreach (var item in movie.Actors)
                        {
                            response.Cast.Add(new StakeholderResponse()
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

        public UserResponse GetUserById(IdentificationInput input)
        {
            UserResponse response = new UserResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var user = MovieOrchUseCases.GetUserById(input.Id);
                if (user != null)
                {
                    response.Id = user.Id;
                    response.Succes = true;
                    response.Email = user.Email;
                    response.Age = user.Age;
                    response.UserName = user.UserName;
                    response.Password = user.Password;
                    if (user.Achievements != null)
                    {
                        foreach (var item in user.Achievements)
                        {
                            response.Achievements.Add(new AchievementResponse()
                            {
                                Id = item.Id,
                                Name = item.Name
                            });
                        }
                    }
                    if (user.Friends != null)
                    {
                        foreach (var item in user.Friends)
                        {
                            response.Friends.Add(new ClientResponse()
                            {
                                Id = item.Id,
                                UserName = item.UserName
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

        public WatchlistResponse GetWatchlistById(IdentificationInput input)
        {
            WatchlistResponse response = new WatchlistResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var watchlist = MovieOrchUseCases.GetWatchlistById(input.Id);
                if (watchlist != null)
                {
                    response.Name = watchlist.Name;
                    response.Id = watchlist.Id;
                    response.Succes = true;
                    if (watchlist.Movies != null)
                    {
                        foreach (var item in watchlist.Movies)
                        {
                            response.Movies.Add(new MovieResponse()
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

        #region delete
        public BaseResponse DeleteAchievement(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.DeleteAchievement(input.Id);
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

        public BaseResponse DeleteActor(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.DeleteActor(input.Id);
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

        public BaseResponse DeleteAdmin(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.DeleteAdmin(input.Id);
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

        public BaseResponse DeleteDirector(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.DeleteDirector(input.Id);
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

        public BaseResponse DeleteMovie(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.DeleteMovie(input.Id);
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

        public BaseResponse DeleteUser(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.DeleteUser(input.Id);
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

        public BaseResponse DeleteWatchlist(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.DeleteWatchlist(input.Id);
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
        public AchievementResponse UpdateAchievement(AchievementInput input)
        {
            AchievementResponse response = new AchievementResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedAchiev = MovieOrchUseCases.UpdateAchievement(new AchievementCriteria()
                {
                    Description = input.Description,
                    Id = input.Id,
                    Name = input.Name
                });
                if (updatedAchiev.Exceptions != null && updatedAchiev.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in updatedAchiev.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Description = updatedAchiev.Description;
                    response.Id = updatedAchiev.Id;
                    response.Name = updatedAchiev.Name;
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

        public StakeholderResponse UpdateActor(StakeholderInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedActor = MovieOrchUseCases.UpdateActor(new StakeholderCriteria()
                {
                    Age = input.Age,
                    CountOscars = input.CountOscars,
                    Description = input.Description,
                    FamilyName = input.FamilyName,
                    FirstName = input.FirstName,
                    Id = input.Id,
                    Sex = input.Sex
                });
                if (updatedActor.Exceptions != null && updatedActor.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in updatedActor.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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

        public ClientResponse UpdateAdmin(ClientInput input)
        {
            ClientResponse response = new ClientResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedAdmin = MovieOrchUseCases.UpdateAdmin(new ClientCriteria()
                {
                    Id = input.Id,
                    Age = input.Age,
                    Email = input.Email,
                    Password = input.ClientPassword,
                    UserName = input.UserName
                });
                if (updatedAdmin.Exceptions != null && updatedAdmin.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in updatedAdmin.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Age = updatedAdmin.Age;
                    response.Email = updatedAdmin.Email;
                    response.Id = updatedAdmin.Id;
                    response.Password = updatedAdmin.Password;
                    response.UserName = updatedAdmin.UserName;
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

        public StakeholderResponse UpdateDirector(StakeholderInput input)
        {
            StakeholderResponse response = new StakeholderResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedDirector = MovieOrchUseCases.UpdateDirector(new StakeholderCriteria()
                {
                    Age = input.Age,
                    CountOscars = input.CountOscars,
                    Description = input.Description,
                    FamilyName = input.FamilyName,
                    FirstName = input.FirstName,
                    Id = input.Id,
                    Sex = input.Sex
                });
                if (updatedDirector.Exceptions != null && updatedDirector.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in updatedDirector.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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

        public MovieResponse UpdateMovie(MovieInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedMovie = MovieOrchUseCases.UpdateMovie(new MovieCriteria()
                {
                    Cover = input.Cover,
                    Description = input.Description,
                    Genre = input.Genre,
                    Id = input.Id,
                    Playtime = input.Playtime,
                    Score = input.Score,
                    Title = input.Title,
                    Year = input.Year
                });
                if (updatedMovie.Exceptions != null && updatedMovie.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in updatedMovie.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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

        public ClientResponse UpdateUser(ClientInput input)
        {
            ClientResponse response = new ClientResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedUser = MovieOrchUseCases.UpdateUser(new ClientCriteria()
                {
                    Id = input.Id,
                    Age = input.Age,
                    Email = input.Email,
                    Password = input.ClientPassword,
                    UserName = input.UserName
                });
                if (updatedUser.Exceptions != null && updatedUser.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in updatedUser.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Age = updatedUser.Age;
                    response.Email = updatedUser.Email;
                    response.Id = updatedUser.Id;
                    response.Password = updatedUser.Password;
                    response.UserName = updatedUser.UserName;
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
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedWatchlist = MovieOrchUseCases.UpdateWatchlist(new WatchlistCriteria()
                {
                    Id = input.Id,
                    Name = input.Name
                });
                if (updatedWatchlist.Exceptions != null && updatedWatchlist.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in updatedWatchlist.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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

        #region relations
        public MovieResponse AddDirectorToMovie(RelationsInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var relation = MovieOrchUseCases.AddDirectorToMovie(new RelationsCriteria()
                {
                    ChildId = input.ChildId,
                    ParentId = input.ParentId
                });
                if (relation.Exceptions != null && relation.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in relation.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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
                        response.Director = new StakeholderResponse()
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
                            response.Cast.Add(new StakeholderResponse()
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

        public MovieResponse AddActorToMovie(RelationsInput input)
        {
            MovieResponse response = new MovieResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var relation = MovieOrchUseCases.AddActorToMovie(new RelationsCriteria()
                {
                    ChildId = input.ChildId,
                    ParentId = input.ParentId,
                    CharacterName = input.CharacterName
                });
                if (relation.Exceptions != null && relation.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in relation.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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
                        response.Director = new StakeholderResponse()
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
                            response.Cast.Add(new StakeholderResponse()
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

        public WatchlistResponse AddMovieToWatchList(RelationsInput input)
        {
            WatchlistResponse response = new WatchlistResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var relation = MovieOrchUseCases.AddMovieToWatchlist(new RelationsCriteria()
                {
                    ParentId = input.ParentId,
                    ChildId = input.ChildId
                });

                if (relation.Exceptions != null && relation.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in relation.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
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
                            response.Movies.Add(new MovieResponse()
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

        public UserResponse AddAchievementToUser(RelationsInput input)
        {
            UserResponse response = new UserResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var relation = MovieOrchUseCases.AddAchievementToUser(new RelationsCriteria()
                {
                    ChildId = input.ChildId,
                    ParentId = input.ParentId
                });

                if (relation.Exceptions != null && relation.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in relation.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
                        });
                    }
                }
                else
                {
                    var user = relation.User;
                    response.Succes = true;
                    response.Id = user.Id;
                    response.Succes = true;
                    response.Email = user.Email;
                    response.Age = user.Age;
                    response.UserName = user.UserName;
                    response.Password = user.Password;
                    if (user.Achievements != null)
                    {
                        foreach (var item in user.Achievements)
                        {
                            response.Achievements.Add(new AchievementResponse()
                            {
                                Id = item.Id,
                                Name = item.Name
                            });
                        }
                    }
                    if (user.Friends != null)
                    {
                        foreach (var item in user.Friends)
                        {
                            response.Friends.Add(new UserResponse()
                            {
                                Id = item.Id,
                                UserName = item.UserName
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

        public UserResponse AddFriend(RelationsInput input)
        {
            UserResponse response = new UserResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var relation = MovieOrchUseCases.AddFriend(new RelationsCriteria()
                {
                    ChildId = input.ChildId,
                    ParentId = input.ParentId
                });
                if (relation.Exceptions != null && relation.Exceptions.Count > 0)
                {
                    response.Succes = false;
                    foreach (var item in relation.Exceptions)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.ErrorMessage,
                            PropertyName = item.PropertyName
                        });
                    }
                }
                else
                {
                    var user = relation.User;
                    response.Succes = true;
                    response.Id = user.Id;
                    response.Succes = true;
                    response.Email = user.Email;
                    response.Age = user.Age;
                    response.UserName = user.UserName;
                    response.Password = user.Password;
                    if (user.Achievements != null)
                    {
                        foreach (var item in user.Achievements)
                        {
                            response.Achievements.Add(new AchievementResponse()
                            {
                                Id = item.Id,
                                Name = item.Name
                            });
                        }
                    }
                    if (user.Friends != null)
                    {
                        foreach (var item in user.Friends)
                        {
                            response.Friends.Add(new UserResponse()
                            {
                                Id = item.Id,
                                UserName = item.UserName
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

        public BaseResponse DeleteFriend(RelationsInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.RemoveFriend(new RelationsCriteria()
                {
                    ChildId = input.ChildId,
                    ParentId = input.ParentId
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

        public BaseResponse DeleteDirectorFromMovie(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.RemoveDirectorFromMovie(input.Id);
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

        public BaseResponse DeleteActorFromMovie(RelationsInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.RemoveActorFromMovie(new RelationsCriteria()
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

        public BaseResponse DeleteMovieFromWatchlist(RelationsInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MovieOrchUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = MovieOrchUseCases.RemoveMovieFromWatchlist(new RelationsCriteria()
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
    }
}
