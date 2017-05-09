using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.CriteriaObjects
{
    [Serializable]
    public class ClientCriteria: CriteriaBase<ClientCriteria>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
    }
}
