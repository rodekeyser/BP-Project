using Csla;
using MovieBusinessLayer.BusinessModel.Editables;
using MovieDataLayer;
using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel.EditableLists
{
    [Serializable]
    public class EditableActorList: BusinessListBase<EditableActorList, EditableActor>
    {
        public static EditableActorList GetAllActors()
        {
            return DataPortal.Fetch<EditableActorList>();
        }

        public static EditableActorList ConvertChild(List<Actor> actors)
        {
            return DataPortal.FetchChild<EditableActorList>(actors);
        }


        private void DataPortal_Fetch()
        {
            List<Actor> actors = new List<Actor>();
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                actors = context.Actor.Include("Movies").Where(x => x.CharacterName == null).ToList();
            }
            if (actors.Count == 0)
                throw new Exception("No actors were found");
            foreach (var item in actors)
            {
                this.Add(EditableActor.ConvertChild(item));
            }
        }

        private void Child_Fetch(List<Actor> actors)
        {
            foreach(var item in actors)
            {
                this.Add(EditableActor.ConvertChild(item));
            }
            MarkAsChild();
        }
    }
}
