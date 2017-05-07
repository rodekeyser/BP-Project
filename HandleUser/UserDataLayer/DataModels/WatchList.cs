using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLayer.DataModels
{
    [Serializable]
    public class Watchlist
    {
        public int Id { get; set; }

        #region relations

        public User User { get; set; }

        #endregion
    }
}
