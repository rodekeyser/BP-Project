using Csla;
using MovieBusinessLayer.BusinessModel.Editables;
using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel.EditableLists
{
    [Serializable]
    public class EditableWatchListList: BusinessListBase<EditableWatchListList, EditableWatchList>
    {
        public static EditableWatchListList GetAllWatchListsForPlayer(List<int> watchLists)
        {
            return DataPortal.Fetch<EditableWatchListList>(watchLists);
        }

        private void DataPortal_Fetch(List<int> watchLists)
        {
            List<WatchList> watchlists = new List<WatchList>();
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                foreach(var item in watchLists)
                {
                    var wl = context.Watchlist.Include("Movies").Where(x => x.Id == item).FirstOrDefault();
                    if (wl != null) {
                        watchlists.Add(wl);
                    }
                    else
                    {
                        throw new Exception("Watchlist with id " + item + " does not exist");
                    }
                }
            }
            if(watchlists.Count == 0)
            {
                throw new Exception("You have no watchlists");
            }
            foreach(var item in watchlists)
            {
                this.Add(EditableWatchList.ConvertChild(item));
            }
        }
    }
}
