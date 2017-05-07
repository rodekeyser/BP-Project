using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using UserBusinessLayer.BusinessModel;
using UserBusinessLayer.BusinessModel.CriteriaObjects;
using UserBusinessLayer.BusinessModel.EditableLists;
using UserBusinessLayer.BusinessModel.Editables;
using UserBusinessLayer.BusinessModel.Helpers;

namespace UserBusinessLayer.UseCases
{
    public static class UserUseCases
    {
        #region create
        public static EditableAchievement CreateAchievement(AchievementCriteria criteria)
        {
            var newAchievement = EditableAchievement.NewEditableAchievement(criteria);
            if (newAchievement.IsValid)
            {
                newAchievement = newAchievement.Save();
            }
            return newAchievement;
        }

        public static EditableAdmin CreateAdmin(ClientCriteria criteria)
        {
            var newAdmin = EditableAdmin.NewEditableAdmin(criteria);
            if (newAdmin.IsValid)
            {
                newAdmin = newAdmin.Save();
            }
            return newAdmin;
        }

        public static EditableUser CreateUser(ClientCriteria criteria)
        {
            var newUser = EditableUser.NewEditableUser(criteria);
            if (newUser.IsValid)
            {
                newUser = newUser.Save();
            }
            return newUser;
        }

        public static EditableWatchlist CreateWatchlist(WatchlistCriteria criteria)
        {
            var newWatchlist = EditableWatchlist.NewEditableWatchlist(criteria);
            if (newWatchlist.IsValid)
            {
                newWatchlist = newWatchlist.Save();
            }
            return newWatchlist;
        }
        #endregion

        #region read
        public static EditableAchievement GetAchievementById(int id)
        {
            try
            {
                return EditableAchievement.GetAchievementById(id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static EditableAdmin GetAdminById(int id)
        {
            try
            {
                return EditableAdmin.GetAdminById(id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static EditableUser GetUserById(int id)
        {
            try
            {
                return EditableUser.GetUserById(id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static EditableAchievementList GetAllAchievements()
        {
            try
            {
                return EditableAchievementList.GetAllAchievements();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static EditableAdminList GetAllAdmins()
        {
            try
            {
                return EditableAdminList.GetAllAdmins();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static EditableUserList GetAllUsers()
        {
            try
            {
                return EditableUserList.GetAllUsers();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region delete
        public static bool DeleteAchievementById(int id)
        {
            try
            {
                EditableAchievement.DeleteAchievementById(id);
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteAdminById(int id)
        {
            try
            {
                EditableAdmin.DeleteAdminById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteUserById(int id)
        {
            try
            {
                EditableUser.DeleteUserById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteWatchlistById(int id)
        {
            try
            {
                EditableWatchlist.DeleteWatchlistById(id);
                return true;
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
            catch(Exception ex)
            {
                throw;
            }
        }

        public static UserToUser AddFriend(RelationsCriteria criteria)
        {
            try
            {
                var userWithFriends = UserToUser.AddUserToUser(criteria);
                if (userWithFriends.IsValid)
                {
                    userWithFriends = userWithFriends.Save();
                }
                return userWithFriends;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteFriend(RelationsCriteria criteria)
        {
            try
            {
                UserToUser.DeleteUserFromUser(criteria);
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region update
        public static EditableAchievement UpdateAchievement(AchievementCriteria criteria)
        {
            try
            {
                var achiev = EditableAchievement.GetAchievementById(criteria.Id);
                achiev.Description = criteria.Description;
                achiev.Name = criteria.Name;
                if (achiev.IsValid)
                {
                    achiev = achiev.Save();
                }
                return achiev;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static EditableAdmin UpdateAdmin(ClientCriteria criteria)
        {
            try
            {
                var admin = EditableAdmin.GetAdminById(criteria.Id);
                admin.Age = criteria.Age;
                admin.Email = criteria.Email;
                if (criteria.Password != null)
                    admin.Password = PasswordHelper.HashPassword(criteria.Password);
                else
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

        public static EditableUser UpdateUser(ClientCriteria criteria)
        {
            try
            {
                var user = EditableUser.GetUserById(criteria.Id);
                user.Age = criteria.Age;
                user.Email = criteria.Email;
                if (criteria.Password != null)
                    user.Password = PasswordHelper.HashPassword(criteria.Password);
                else
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
