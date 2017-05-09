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
    public class NamedAdminList: BusinessListBase<NamedAdminList, NamedAdmin>
    {
        public static NamedAdminList GetAllAdmins()
        {
            return DataPortal.Fetch<NamedAdminList>();
        }

        public static NamedAdminList ConvertChild(List<ClientResponse> admins)
        {
            return DataPortal.FetchChild<NamedAdminList>(admins);
        }

        private void DataPortal_Fetch()
        {
            ClientListResponse admins = new ClientListResponse();
            using (var service = new UserServiceClient())
            {
                admins = service.GetAllAdmins(new BaseInput() {
                    Application = "user",
                    Password = "user"
                });
            }
            if (!admins.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in admins.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
            foreach (var item in admins.Clients)
            {
                this.Add(NamedAdmin.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<ClientResponse> admins)
        {
            foreach (var item in admins)
            {
                this.Add(NamedAdmin.ConvertChild(item));
            }
            MarkAsChild();
        }
    }
}
