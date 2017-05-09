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
    public class NamedAchievementList: BusinessListBase<NamedAchievementList, NamedAchievement>
    {
        public static NamedAchievementList GetAllAchievements()
        {
            return DataPortal.Fetch<NamedAchievementList>();
        }

        public static NamedAchievementList ConvertChild(List<AchievementResponse> responses)
        {
            return DataPortal.FetchChild<NamedAchievementList>(responses);
        }

        private void DataPortal_Fetch()
        {
            AchievementListResponse achieves = new AchievementListResponse();
            using (var service = new UserServiceClient())
            {
                achieves = service.GetAllAchievements(new BaseInput() {
                    Application = "user",
                    Password = "user"
                });
            }
            if (!achieves.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in achieves.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
            foreach (var item in achieves.Achievements)
            {
                this.Add(NamedAchievement.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<AchievementResponse> responses)
        {
            foreach (var item in responses)
            {
                this.Add(NamedAchievement.ConvertChild(item));
            }
        }
    }
}
