using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataLayer.DataModels
{
    [Serializable]
    public class Achievement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #region relations

        public List<User> Users { get; set; }

        #endregion
    }
}
