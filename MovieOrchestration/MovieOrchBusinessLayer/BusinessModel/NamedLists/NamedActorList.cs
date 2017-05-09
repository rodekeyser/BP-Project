using Csla;
using MovieOrchBusinessLayer.BusinessModel.NamedObjects;
using MovieOrchBusinessLayer.MovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.NamedLists
{
    [Serializable]
    public class NamedActorList: BusinessListBase<NamedActorList, NamedActor>
    {
        public static NamedActorList GetAllActors()
        {
            return DataPortal.Fetch<NamedActorList>();
        }

        public static NamedActorList ConvertChild(List<ChildStakeholderResponse> responses)
        {
            return DataPortal.FetchChild<NamedActorList>(responses);
        }

        private void DataPortal_Fetch()
        {
            StakeholderListResponse actors = new StakeholderListResponse();
            using (var service = new MovieServiceClient())
            {
                actors = service.GetAllActors(new BaseInput() {
                    Application = "movie",
                    Password = "movie"
                });
            }
            if (!actors.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in actors.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
            foreach (var item in actors.Stakeholders)
            {
                this.Add(NamedActor.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<ChildStakeholderResponse> responses)
        {
            foreach (var item in responses)
            {
                this.Add(NamedActor.ConvertChild(item));
            }
        }
    }
}
