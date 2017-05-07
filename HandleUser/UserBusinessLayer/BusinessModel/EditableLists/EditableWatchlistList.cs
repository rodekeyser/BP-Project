using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBusinessLayer.BusinessModel.Editables;
using UserDataLayer.DataModels;

namespace UserBusinessLayer.BusinessModel.EditableLists
{
    [Serializable]
    public class EditableWatchlistList: BusinessListBase<EditableWatchlistList, EditableWatchlist>
    {
        public static EditableWatchlistList ConvertChild(List<Watchlist> wls)
        {
            return DataPortal.FetchChild<EditableWatchlistList>(wls);
        }

        private void Child_Fetch(List<Watchlist> wls)
        {
            foreach(var item in wls)
            {
                this.Add(EditableWatchlist.ConvertChild(item));
            }
        }
    }
}
