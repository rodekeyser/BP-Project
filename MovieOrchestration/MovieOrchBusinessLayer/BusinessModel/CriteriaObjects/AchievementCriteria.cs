using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.CriteriaObjects
{
    [Serializable]
    public class AchievementCriteria: CriteriaBase<AchievementCriteria>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
