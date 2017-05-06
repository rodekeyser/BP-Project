using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel.CriteriaObjects
{
    [Serializable]
    public class WatchlistCriteria: CriteriaBase<WatchlistCriteria>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
