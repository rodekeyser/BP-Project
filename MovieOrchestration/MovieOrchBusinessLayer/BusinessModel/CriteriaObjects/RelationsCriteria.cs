﻿using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.CriteriaObjects
{
    [Serializable]
    public class RelationsCriteria: CriteriaBase<RelationsCriteria>
    {
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public string CharacterName { get; set; }
    }
}