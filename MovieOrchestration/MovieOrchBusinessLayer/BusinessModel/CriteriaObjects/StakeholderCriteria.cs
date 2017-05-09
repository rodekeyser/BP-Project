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
    public class StakeholderCriteria: CriteriaBase<StakeholderCriteria>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Description { get; set; }
        public int CountOscars { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
    }
}
