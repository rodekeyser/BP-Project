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
    public class EditableAchievementList: BusinessListBase<EditableAchievementList, EditableAchievement>
    {
        public static EditableAchievementList GetAllAchievements()
        {
            return DataPortal.Fetch<EditableAchievementList>();
        }

        public static EditableAchievementList ConvertChild(List<Achievement> achieves)
        {
            return DataPortal.FetchChild<EditableAchievementList>(achieves);
        }

        private void DataPortal_Fetch()
        {
            List<Achievement> achieves = new List<Achievement>();
            using(var context = new UserDataLayer.DbContext.UserDbContext())
            {
                achieves = context.Achievement.ToList();
            }
            if (achieves.Count == 0)
                throw new Exception("There were no achievements found");
            foreach(var item in achieves)
            {
                this.Add(EditableAchievement.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<Achievement> achieves)
        {
            foreach(var item in achieves)
            {
                this.Add(EditableAchievement.ConvertChild(item));
            }
        }
    }
}
