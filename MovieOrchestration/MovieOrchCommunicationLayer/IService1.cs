using MovieOrchCommunicationLayer.DataContracts.Input;
using MovieOrchCommunicationLayer.DataContracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MovieOrchCommunicationLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        #region create
        [OperationContract]
        IdentificationResponse CreateAchievement(AchievementInput input);
        [OperationContract]
        IdentificationResponse CreateActor(StakeholderInput input);
        [OperationContract]
        IdentificationResponse CreateAdmin(ClientInput input);
        [OperationContract]
        IdentificationResponse CreateDirector(StakeholderInput input);
        [OperationContract]
        IdentificationResponse CreateMovie(MovieInput input);
        [OperationContract]
        IdentificationResponse CreateUser(ClientInput input);
        [OperationContract]
        IdentificationResponse CreateWatchlist(WatchlistInput input);
        #endregion

        #region read
        [OperationContract]
        AchievementListResponse GetAllAchievements(BaseInput input);
        [OperationContract]
        StakeholderListResponse GetAllActors(BaseInput input);
        [OperationContract]
        ClientListResponse GetAllAdmins(BaseInput input);
        [OperationContract]
        StakeholderListResponse GetAllDirectors(BaseInput input);
        [OperationContract]
        MovieListResponse GetAllMovies(BaseInput input);
        [OperationContract]
        ClientListResponse GetAllUsers(BaseInput input);
        [OperationContract]
        WatchlistListResponse GetAllWatchlistsOfUser(IdentificationInput input);
        [OperationContract]
        AchievementResponse GetAchievementById(IdentificationInput input);
        [OperationContract]
        StakeholderResponse GetActorById(IdentificationInput input);
        [OperationContract]
        ClientResponse GetAdminById(IdentificationInput input);
        [OperationContract]
        StakeholderResponse GetDirectorById(IdentificationInput input);
        [OperationContract]
        MovieResponse GetMovieById(IdentificationInput input);
        [OperationContract]
        UserResponse GetUserById(IdentificationInput input);
        [OperationContract]
        WatchlistResponse GetWatchlistById(IdentificationInput input);
        #endregion

        #region delete
        [OperationContract]
        BaseResponse DeleteAchievement(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteActor(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteAdmin(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteDirector(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteMovie(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteUser(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteWatchlist(IdentificationInput input);
        #endregion

        #region update
        [OperationContract]
        AchievementResponse UpdateAchievement(AchievementInput input);
        [OperationContract]
        StakeholderResponse UpdateActor(StakeholderInput input);
        [OperationContract]
        ClientResponse UpdateAdmin(ClientInput input);
        [OperationContract]
        StakeholderResponse UpdateDirector(StakeholderInput input);
        [OperationContract]
        MovieResponse UpdateMovie(MovieInput input);
        [OperationContract]
        ClientResponse UpdateUser(ClientInput input);
        [OperationContract]
        WatchlistResponse UpdateWatchlist(WatchlistInput input);
        #endregion

        #region relations
        [OperationContract]
        MovieResponse AddDirectorToMovie(RelationsInput input);
        [OperationContract]
        MovieResponse AddActorToMovie(RelationsInput input);
        [OperationContract]
        WatchlistResponse AddMovieToWatchList(RelationsInput input);
        [OperationContract]
        UserResponse AddAchievementToUser(RelationsInput input);
        [OperationContract]
        UserResponse AddFriend(RelationsInput input);
        [OperationContract]
        BaseResponse DeleteFriend(RelationsInput input);
        [OperationContract]
        BaseResponse DeleteDirectorFromMovie(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteActorFromMovie(RelationsInput input);
        [OperationContract]
        BaseResponse DeleteMovieFromWatchlist(RelationsInput input);
        #endregion
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

}
