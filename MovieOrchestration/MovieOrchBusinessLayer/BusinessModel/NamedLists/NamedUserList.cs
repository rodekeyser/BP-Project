using Csla;
using MovieOrchBusinessLayer.BusinessModel.NamedObjects;
using MovieOrchBusinessLayer.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.NamedLists
{
    [Serializable]
    public class NamedUserList: BusinessListBase<NamedUserList, NamedUser>
    {
        public static NamedUserList GetAllUsers()
        {
            return DataPortal.Fetch<NamedUserList>();
        }

        public static NamedUserList ConvertChild(List<ClientResponse> users)
        {
            return DataPortal.FetchChild<NamedUserList>(users);
        }

        private void DataPortal_Fetch()
        {
            ClientListResponse users = new ClientListResponse();
            using (var service = new UserServiceClient())
            {
                users = service.GetAllUsers(new BaseInput() {
                    Application = "user",
                    Password = "user"
                });
            }
            if (!users.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in users.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
            foreach (var item in users.Clients)
            {
                this.Add(NamedUser.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<ClientResponse> users)
        {
            foreach (var item in users)
            {
                this.Add(NamedUser.ConvertChild(item));
            }
            MarkAsChild();
        }
    }
}
