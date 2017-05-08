using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UserBusinessLayer.BusinessModel.CriteriaObjects;
using UserBusinessLayer.UseCases;
using UserCommunicationLayer.DataContracts.Input;
using UserCommunicationLayer.DataContracts.Response;

namespace UserCommunicationLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        #region create
        public IdentificationResponse CreateAchievement(AchievementInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newAchievement = UserUseCases.CreateAchievement(new AchievementCriteria()
                {
                    Description = input.Description,
                    Name = input.Name
                });
                if (!newAchievement.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in newAchievement.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description,
                            PropertyName = item.Property
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

        public IdentificationResponse CreateAdmin(ClientInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newAdmin = UserUseCases.CreateAdmin(new ClientCriteria() {
                     Age = input.Age,
                     Email = input.Email,
                     Password = input.ClientPassword,
                     UserName = input.UserName
                });
                if (!newAdmin.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in newAdmin.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description,
                            PropertyName = item.Property
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

        public IdentificationResponse CreateUser(ClientInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newUser = UserUseCases.CreateUser(new ClientCriteria()
                {
                    Age = input.Age,
                    Email = input.Email,
                    Password = input.ClientPassword,
                    UserName = input.UserName
                });
                if (!newUser.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in newUser.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description,
                            PropertyName = item.Property
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

        public IdentificationResponse AddWatchListToUser(WatchlistInput input)
        {
            IdentificationResponse response = new IdentificationResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var newWatchlist = UserUseCases.CreateWatchlist(new WatchlistCriteria()
                {
                    Id = input.Id,
                    UserId = input.UserId
                });
                if (!newWatchlist.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in newWatchlist.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description,
                            PropertyName = item.Property
                        });
                    }
                }
                else
                {
                    response.Succes = true;
                    response.Id = newWatchlist.Id;
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
        public BaseResponse DeleteAchievementById(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = UserUseCases.DeleteAchievementById(input.Id);
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

        public BaseResponse DeleteAdminById(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = UserUseCases.DeleteAdminById(input.Id);
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

        public BaseResponse DeleteUserById(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = UserUseCases.DeleteUserById(input.Id);
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

        public BaseResponse DeleteWatchlistById(IdentificationInput input)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = UserUseCases.DeleteWatchlistById(input.Id);
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
        public AchievementResponse GetAchievementById(IdentificationInput input)
        {
            AchievementResponse response = new AchievementResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var achievement = UserUseCases.GetAchievementById(input.Id);
                if (achievement != null)
                {
                    response.Succes = true;
                    response.Description = achievement.Description;
                    response.Id = achievement.Id;
                    response.Name = achievement.Name;
                    if(achievement.Users != null)
                    {
                        foreach(var item in achievement.Users)
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

        public ClientResponse GetAdminById(IdentificationInput input)
        {
            ClientResponse response = new ClientResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var admin = UserUseCases.GetAdminById(input.Id);
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

        public UserResponse GetUserById(IdentificationInput input)
        {
            UserResponse response = new UserResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var user = UserUseCases.GetUserById(input.Id);
                if(user != null)
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
                    if(user.Watchlists != null)
                    {
                        foreach(var item in user.Watchlists)
                        {
                            response.Watchlists.Add(new WatchlistResponse()
                            {
                                Id = item.Id
                            });
                        }
                    }
                    if(user.Friends != null)
                    {
                        foreach(var item in user.Friends)
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

        public AchievementListResponse GetAllAchievements(BaseInput input)
        {
            AchievementListResponse response = new AchievementListResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var achievements = UserUseCases.GetAllAchievements();
                if(achievements != null)
                {
                    response.Succes = true;
                    foreach(var item in achievements)
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

        public ClientListResponse GetAllAdmins(BaseInput input)
        {
            ClientListResponse response = new ClientListResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var admins = UserUseCases.GetAllAdmins();
                if (admins != null)
                {
                    response.Succes = true;
                    foreach(var item in admins)
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

        public ClientListResponse GetAllUsers(BaseInput input)
        {
            ClientListResponse response = new ClientListResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var users = UserUseCases.GetAllUsers();
                if (users != null)
                {
                    response.Succes = true;
                    foreach(var item in users)
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
        #endregion

        #region relations
        public UserResponse AddAchievementToUser(RelationsInput input)
        {
            UserResponse response = new UserResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var userWithChildren = UserUseCases.AddAchievementToUser(new RelationsCriteria()
                {
                    ChildId = input.ChildId,
                    ParentId = input.ParentId
                });
                if (!userWithChildren.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in userWithChildren.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description,
                            PropertyName = item.Property
                        });
                    }
                }
                else
                {
                    var user = userWithChildren.User;
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
                    if (user.Watchlists != null)
                    {
                        foreach (var item in user.Watchlists)
                        {
                            response.Watchlists.Add(new WatchlistResponse()
                            {
                                Id = item.Id
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

        public UserResponse AddFriend(RelationsInput input)
        {
            UserResponse response = new UserResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var userWithFriends = UserUseCases.AddFriend(new RelationsCriteria()
                {
                    ChildId = input.ChildId,
                    ParentId = input.ParentId
                });
                if (!userWithFriends.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in userWithFriends.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description,
                            PropertyName = item.Property
                        });
                    }
                }
                else
                {
                    var user = userWithFriends.User;
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
                    if (user.Watchlists != null)
                    {
                        foreach (var item in user.Watchlists)
                        {
                            response.Watchlists.Add(new WatchlistResponse()
                            {
                                Id = item.Id
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
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                response.Succes = UserUseCases.DeleteFriend(new RelationsCriteria()
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
        #endregion

        #region update
        public AchievementResponse UpdateAchievement(AchievementInput input)
        {
            AchievementResponse response = new AchievementResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedAchiev = UserUseCases.UpdateAchievement(new AchievementCriteria()
                {
                    Description = input.Description,
                    Id = input.Id,
                    Name = input.Name
                });
                if (!updatedAchiev.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in updatedAchiev.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description,
                            PropertyName = item.Property
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

        public ClientResponse UpdateAdmin(ClientInput input)
        {
            ClientResponse response = new ClientResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedAdmin = UserUseCases.UpdateAdmin(new ClientCriteria()
                {
                    Id = input.Id,
                    Age = input.Age,
                    Email = input.Email,
                    Password = input.ClientPassword,
                    UserName = input.UserName
                });
                if (!updatedAdmin.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in updatedAdmin.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description,
                            PropertyName = item.Property
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

        public UserResponse UpdateUser(ClientInput input)
        {
            UserResponse response = new UserResponse();
            try
            {
                UserUseCases.ValidateIncomingRequest(input.Application, input.Password);
                var updatedUser = UserUseCases.UpdateUser(new ClientCriteria()
                {
                    Id = input.Id,
                    Age = input.Age,
                    Email = input.Email,
                    Password = input.ClientPassword,
                    UserName = input.UserName
                });
                if (!updatedUser.IsValid)
                {
                    response.Succes = false;
                    foreach (var item in updatedUser.BrokenRulesCollection)
                    {
                        response.Errors.Add(new Error()
                        {
                            ErrorMessage = item.Description,
                            PropertyName = item.Property
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
        #endregion
    }
}
