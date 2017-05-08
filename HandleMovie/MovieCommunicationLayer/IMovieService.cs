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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMovieService" in both code and config file together.
    [ServiceContract]
    public interface IMovieService
    {
        #region create
        [OperationContract]
        IdentificationResponse CreateWatchlist(WatchlistInput input);
        [OperationContract]
        IdentificationResponse CreateMovie(MovieInput input);
        [OperationContract]
        IdentificationResponse CreateActor(StakeholderInput input);
        [OperationContract]
        IdentificationResponse CreateDirector(StakeholderInput input);
        #endregion

        #region read
        [OperationContract]
        WatchlistResponse GetWatchlistById(IdentificationInput input);
        [OperationContract]
        MovieResponse GetMovieById(IdentificationInput input);
        [OperationContract]
        StakeholderResponse GetDirectorById(IdentificationInput input);
        [OperationContract]
        StakeholderResponse GetActorById(IdentificationInput input);
        [OperationContract]
        WatchlistListResponse GetAllWatchlistsOfUser(MultipleIdentificationsInput input);
        [OperationContract]
        MovieListResponse GetAllMovies(BaseInput input);
        [OperationContract]
        StakeholderListResponse GetAllDirectors(BaseInput input);
        [OperationContract]
        StakeholderListResponse GetAllActors(BaseInput input);
        #endregion

        #region delete
        [OperationContract]
        BaseResponse DeleteWatchlistById(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteMovieById(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteDirectorById(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteActorById(IdentificationInput input);
        #endregion

        #region addrelation
        [OperationContract]
        MovieResponse AddDirectorToMovie(RelationInput input);
        [OperationContract]
        MovieResponse AddActorToMovie(RelationInput input);
        [OperationContract]
        WatchlistResponse AddMovieToWatchList(RelationInput input);
        [OperationContract]
        BaseResponse DeleteDirectorFromMovie(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteActorFromMovie(RelationInput input);
        [OperationContract]
        BaseResponse DeleteMovieFromWatchlist(RelationInput input);
        #endregion

        #region update
        [OperationContract]
        StakeholderResponse UpdateActor(StakeholderInput input);
        [OperationContract]
        WatchlistResponse UpdateWatchlist(WatchlistInput input);
        [OperationContract]
        MovieResponse UpdateMovie(MovieInput input);
        #endregion
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

}
