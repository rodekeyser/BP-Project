using Csla;
using MovieOrchBusinessLayer.BusinessModel.NamedObjects;
using MovieOrchBusinessLayer.MovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.NamedLists
{
    [Serializable]
    public class NamedWatchlistList: BusinessListBase<NamedWatchlistList, NamedWatchlist>
    {
        public static NamedWatchlistList GetAllWatchListsForPlayer(int userId)
        {
            return DataPortal.Fetch<NamedWatchlistList>(userId);
        }

        private void DataPortal_Fetch(int userId)
        {
            List<int> ids = new List<int>();
            WatchlistListResponse watchlists = new WatchlistListResponse();
            using (var service = new UserService.UserServiceClient())
            {
                var user = service.GetUserById(new UserService.IdentificationInput() {
                    Id = userId,
                    Application = "user",
                    Password = "user"
                });
                if (!user.Succes)
                {
                    StringBuilder message = new StringBuilder();
                    foreach (var item in user.Errors)
                    {
                        message.Append(item.ErrorMessage);

                    }
                    throw new Exception(message.ToString());
                }

                if (user.Watchlists != null && user.Watchlists.ToList().Count > 0)
                {
                    foreach(var item in user.Watchlists)
                    {
                        ids.Add(item.Id);
                    }
                }
            }
            using( var service = new MovieServiceClient())
            {
                watchlists = service.GetAllWatchlistsOfUser(new MultipleIdentificationsInput() {
                    Ids = ids.ToArray(),
                    Application = "movie",
                    Password = "movie"
                });
            }
            if (!watchlists.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in watchlists.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
            foreach (var item in watchlists.Watchlists)
            {
                this.Add(NamedWatchlist.ConvertChild(item));
            }
        }
    }
}
