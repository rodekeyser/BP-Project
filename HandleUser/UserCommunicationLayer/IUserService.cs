using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UserCommunicationLayer.DataContracts.Input;
using UserCommunicationLayer.DataContracts.Response;

namespace UserCommunicationLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        #region create
        [OperationContract]
        IdentificationResponse CreateAchievement(AchievementInput input);
        [OperationContract]
        IdentificationResponse CreateAdmin(ClientInput input);
        [OperationContract]
        IdentificationResponse CreateUser(ClientInput input);
        [OperationContract]
        IdentificationResponse AddWatchListToUser(WatchlistInput input);
        #endregion

        #region delete
        [OperationContract]
        BaseResponse DeleteAchievementById(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteAdminById(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteUserById(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteWatchlistById(IdentificationInput input);
        #endregion

        #region read
        [OperationContract]
        AchievementResponse GetAchievementById(IdentificationInput input);
        [OperationContract]
        ClientResponse GetAdminById(IdentificationInput input);
        [OperationContract]
        UserResponse GetUserById(IdentificationInput input);
        [OperationContract]
        AchievementListResponse GetAllAchievements(BaseInput input);
        [OperationContract]
        ClientListResponse GetAllAdmins(BaseInput input);
        [OperationContract]
        ClientListResponse GetAllUsers(BaseInput input);
        #endregion

        #region relations
        [OperationContract]
        UserResponse AddAchievementToUser(RelationsInput input);
        [OperationContract]
        UserResponse AddFriend(RelationsInput input);
        [OperationContract]
        BaseResponse DeleteFriend(RelationsInput input);
        #endregion

        #region update
        [OperationContract]
        AchievementResponse UpdateAchievement(AchievementInput input);
        [OperationContract]
        ClientResponse UpdateAdmin(ClientInput input);
        [OperationContract]
        ClientResponse UpdateUser(ClientInput input);
        #endregion
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

}
