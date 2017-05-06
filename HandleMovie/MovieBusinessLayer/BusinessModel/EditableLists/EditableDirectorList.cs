using Csla;
using MovieBusinessLayer.BusinessModel.Editables;
using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel.EditableLists
{
    [Serializable]
    public class EditableDirectorList: BusinessListBase<EditableDirectorList, EditableDirector>
    {
        public static EditableDirectorList GetAllDirectors()
        {
            return DataPortal.Fetch<EditableDirectorList>();
        }

        private void DataPortal_Fetch()
        {
            List<Director> directors = new List<Director>();
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                directors = context.Director.ToList();
            }
            if(directors.Count == 0)
                throw new Exception("No directors were found");
            foreach(var item in directors)
            {
                this.Add(EditableDirector.ConvertChild(item));
            }
        }
    }
}
