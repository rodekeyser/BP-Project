using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataModels
{
    [Serializable]
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public int Year { get; set; }
        public decimal Score { get; set; }
        public Genre Genre { get; set; }
        public string Description { get; set; }
        public int Playtime { get; set; }

        #region relations
        public List<Actor> Cast { get; set; }
        public Director Director { get; set; }
        public List<WatchList> Watchlists { get; set; }
        #endregion
    }
}
