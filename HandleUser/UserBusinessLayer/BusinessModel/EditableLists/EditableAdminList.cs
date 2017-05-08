using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBusinessLayer.BusinessModel.Editables;
using UserDataLayer.DataModels;

namespace UserBusinessLayer.BusinessModel.EditableLists
{
    [Serializable]
    public class EditableAdminList: BusinessListBase<EditableAdminList, EditableAdmin>
    {
        public static EditableAdminList GetAllAdmins()
        {
            return DataPortal.Fetch<EditableAdminList>();
        }

        public static EditableAdminList ConvertChild(List<Admin> admins)
        {
            return DataPortal.FetchChild<EditableAdminList>(admins);
        }

        private void DataPortal_Fetch()
        {
            List<Admin> admins = new List<Admin>();
            using (var context = new UserDataLayer.DbContext.UserDbContext())
            {
                admins = context.Admin.ToList();
            }
            if (admins.Count == 0)
                throw new Exception("There were no admins found");
            foreach (var item in admins)
            {
                this.Add(EditableAdmin.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<Admin> admins)
        {
            foreach (var item in admins)
            {
                this.Add(EditableAdmin.ConvertChild(item));
            }
            MarkAsChild();
        }
    }
}
