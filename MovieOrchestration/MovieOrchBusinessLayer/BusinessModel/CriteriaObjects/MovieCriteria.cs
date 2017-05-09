using Csla;
using MovieOrchBusinessLayer.MovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.CriteriaObjects
{
    [Serializable]
    public class MovieCriteria:CriteriaBase<MovieCriteria>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public int Year { get; set; }
        public decimal Score { get; set; }
        public Genre Genre { get; set; }
        public string Description { get; set; }
        public int Playtime { get; set; }
    }
}
