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
        IdentificationResponse CreateMovie(MovieInput input);
        [OperationContract]
        IdentificationResponse CreateActor(StakeholderInput input);
        [OperationContract]
        IdentificationResponse CreateDirector(StakeholderInput input);
        [OperationContract]
        IdentificationResponse CreateWatchlist(WatchlistInput input);
        #endregion

        #region read
        [OperationContract]
        StakeholderResponse GetActor(IdentificationInput input);
        [OperationContract]
        StakeholderResponse GetDirector(IdentificationInput input);
        [OperationContract]
        MovieResponse GetMovie(IdentificationInput input);
        [OperationContract]
        WatchlistResponse GetWatchlist(IdentificationInput input);
        [OperationContract]
        StakeholderListResponse GetAllActors(BaseInput input);
        [OperationContract]
        StakeholderListResponse GetAllDirectors(BaseInput input);
        [OperationContract]
        MovieListResponse GetAllMovies(BaseInput input);
        [OperationContract]
        WatchlistListResponse GetAllWatchlistsForUser(MultipleIdentificationsInput input);
        #endregion

        #region delete
        [OperationContract]
        BaseResponse DeleteActor(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteDirector(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteMovie(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteWatchlist(IdentificationInput input);
        #endregion

        #region update
        [OperationContract]
        StakeholderResponse UpdateActor(StakeholderInput input);
        [OperationContract]
        StakeholderResponse UpdateDirector(StakeholderInput input);
        [OperationContract]
        MovieResponse UpdateMovie(MovieInput input);
        [OperationContract]
        WatchlistResponse UpdateWatchlist(WatchlistInput input);
        #endregion

        #region relations
        [OperationContract]
        MovieResponse AddActorToMovie(RelationInput input);
        [OperationContract]
        MovieResponse AddDirectorMovie(RelationInput input);
        [OperationContract]
        WatchlistResponse AddMovieToWatchlist(RelationInput input);
        [OperationContract]
        BaseResponse DeleteActorFromMovie(RelationInput input);
        [OperationContract]
        BaseResponse DeleteDirectorFromMovie(IdentificationInput input);
        [OperationContract]
        BaseResponse DeleteMovieFromWatchlist(RelationInput input);
        #endregion
    }
}
