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
    public class EditableUserList: BusinessListBase<EditableUserList, EditableUser>
    {
        public static EditableUserList GetAllUsers()
        {
            return DataPortal.Fetch<EditableUserList>();
        }

        public static EditableUserList ConvertChild(List<User> users)
        {
            return DataPortal.FetchChild<EditableUserList>(users);
        }

        private void DataPortal_Fetch()
        {
            List<User> users = new List<User>();
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                users = context.User.ToList();
            }
            if (users.Count == 0)
                throw new Exception("There were no users found");
            foreach(var item in users)
            {
                this.Add(EditableUser.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<User> users)
        {
            foreach(var item in users)
            {
                this.Add(EditableUser.ConvertChild(item));
            }
            MarkAsChild();
        }
    }
}
