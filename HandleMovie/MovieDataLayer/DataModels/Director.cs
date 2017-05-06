using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataModels
{
    [Serializable]
    public class Director: Stakeholder
    {
        #region relations
        public List<Movie> Movies { get; set; }
        #endregion
    }
}
