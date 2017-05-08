using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataModels
{
    [Serializable]
    public class Actor: Stakeholder
    {
        public string CharacterName { get; set; }
        #region relations
        public List<Movie> Movies { get; set; }
        #endregion
    }
}
