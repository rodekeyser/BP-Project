using Csla;
using MovieBusinessLayer.BusinessModel.CriteriaObjects;
using MovieBusinessLayer.BusinessModel.Editables;
using MovieDataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel
{
    [Serializable]
    public class ActorToMovie: BusinessBase<ActorToMovie>
    {
        public static readonly PropertyInfo<int> MovieIdProperty = RegisterProperty<int>(c => c.MovieId);
        public int MovieId
        {
            get { return GetProperty(MovieIdProperty); }
            set { SetProperty(MovieIdProperty, value); }
        }
        public static readonly PropertyInfo<int> ActorIdProperty = RegisterProperty<int>(c => c.ActorId);
        public int ActorId
        {
            get { return GetProperty(ActorIdProperty); }
            set { SetProperty(ActorIdProperty, value); }
        }
        public static readonly PropertyInfo<string> CharacterNameProperty = RegisterProperty<string>(c => c.CharacterName);
        public string CharacterName
        {
            get { return GetProperty(CharacterNameProperty); }
            set { SetProperty(CharacterNameProperty, value); }
        }
        public static readonly PropertyInfo<EditableMovie> MovieProperty = RegisterProperty<EditableMovie>(c => c.Movie);
        public EditableMovie Movie
        {
            get { return GetProperty(MovieProperty); }
            set { SetProperty(MovieProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(CharacterNameProperty, "You need to add the character name"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(MovieIdProperty, "Please select a movie"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(ActorIdProperty, "Please select an actor"));
        }

        public static ActorToMovie AddActorToMovie(RelationsCriteria criteria)
        {
            return DataPortal.Create<ActorToMovie>(criteria);
        }

        public static void DeleteActorFromMovie(RelationsCriteria criteria)
        {
            DataPortal.Delete<ActorToMovie>(criteria);
        }

        protected void DataPortal_Create(RelationsCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.ActorId = criteria.ChildId;
                this.MovieId = criteria.ParentId;
                this.CharacterName = criteria.CharacterName;
            }
            base.DataPortal_Create();
        }

        private void DataPortal_Delete(RelationsCriteria criteria)
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                var movie = context.Movie.Include("Cast").Where(x => x.Id == criteria.ParentId).FirstOrDefault();
                var actor = context.Actor.Include("Movies").Where(x => x.Id == criteria.ChildId).FirstOrDefault();
                var actors = context.Actor.Where(x => x.CharacterName == null);
                if (movie == null)
                    throw new Exception("There was no movie found");
                if (actor == null)
                    throw new Exception("There was no actor found");
                if (movie.Cast.Contains(actor))
                {
                    List<Actor> actorToUpdate = new List<Actor>();
                    foreach(var item in actors)
                    {
                        if (item.Movies != null)
                        {
                            if (item.Movies.Contains(movie) && item.FirstName == actor.FirstName && item.FamilyName == actor.FamilyName)
                            {
                                actorToUpdate.Add(item);
                            }
                        }
                    }
                    if(actorToUpdate.Count > 1)
                    {
                        throw new Exception("Something went wrong, you should not add more than two actors with the same name");
                    }
                    actorToUpdate[0].Movies.Remove(movie);
                    context.SaveChanges();
                    context.Actor.Remove(actor);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This movie does not contain the selected actor");
                }
            }
        }

        protected override void DataPortal_Insert()
        {
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                var movie = context.Movie.Include("Cast").Where(x => x.Id == this.MovieId).FirstOrDefault();
                var actor = context.Actor.Include("Movies").Where(x => x.Id == this.ActorId).FirstOrDefault();
                if (movie == null)
                    throw new Exception("There was no movie found");
                if (actor == null)
                    throw new Exception("There was no actor found");
                if (actor.Movies == null)
                {
                    actor.Movies = new List<Movie>();
                }
                if (!actor.Movies.Contains(movie))
                {
                    actor.Movies.Add(movie);
                    context.SaveChanges();
                    Actor newActor = new Actor();
                    newActor = actor;
                    newActor.CharacterName = this.CharacterName;
                    context.Actor.Add(newActor);
                    if (movie.Cast == null)
                    {
                        movie.Cast = new List<Actor>();
                    }
                    movie.Cast.Add(newActor);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The movie already has this actor");
                }
                this.Movie = EditableMovie.GetMovieById(this.MovieId);
            }
        }
    }
}
