using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLayer.DataModels
{
    [Serializable]
    public class User: Client
    {
        #region relations

        public List<Watchlist> Watchlists { get; set; }
        public List<Achievement> Achievements { get; set; }
        public List<User> Friends { get; set; }

        #endregion
    }
}
