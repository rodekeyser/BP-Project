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
    public class NamedDirectorList: BusinessListBase<NamedDirectorList, NamedDirector>
    {
        public static NamedDirectorList GetAllDirectors()
        {
            return DataPortal.Fetch<NamedDirectorList>();
        }

        private void DataPortal_Fetch()
        {
            StakeholderListResponse directors = new StakeholderListResponse();
            using (var service = new MovieServiceClient())
            {
                directors = service.GetAllDirectors(new BaseInput() {
                    Application = "movie",
                    Password = "movie"
                });
            }
            if (!directors.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in directors.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
            foreach (var item in directors.Stakeholders)
            {
                this.Add(NamedDirector.ConvertChild(item));
            }
        }
    }
}
