using Csla;
using Csla.Rules;
using MovieBusinessLayer.BusinessModel.CriteriaObjects;
using MovieBusinessLayer.BusinessModel.EditableLists;
using MovieDataLayer.DataModels;
using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessModel.Editables
{
    [Serializable]
    public class EditableActor: BusinessBase<EditableActor>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);
        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
            set { SetProperty(FirstNameProperty, value); }
        }

        public static readonly PropertyInfo<string> FamilyNameProperty = RegisterProperty<string>(c => c.FamilyName);
        public string FamilyName
        {
            get { return GetProperty(FamilyNameProperty); }
            set { SetProperty(FamilyNameProperty, value); }
        }
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<int> CountOscarsProperty = RegisterProperty<int>(c => c.CountOscars);
        public int CountOscars
        {
            get { return GetProperty(CountOscarsProperty); }
            set { SetProperty(CountOscarsProperty, value); }
        }

        public static readonly PropertyInfo<int> AgeProperty = RegisterProperty<int>(c => c.Age);
        public int Age
        {
            get { return GetProperty(AgeProperty); }
            set { SetProperty(AgeProperty, value); }
        }

        public static readonly PropertyInfo<Sex> SexProperty = RegisterProperty<Sex>(c => c.Sex);
        public Sex Sex
        {
            get { return GetProperty(SexProperty); }
            set { SetProperty(SexProperty, value); }
        }
        public static readonly PropertyInfo<string> CharacterNameProperty = RegisterProperty<string>(c => c.CharacterName);
        public string CharacterName
        {
            get { return GetProperty(CharacterNameProperty); }
            set { SetProperty(CharacterNameProperty, value); }
        }
        public static readonly PropertyInfo<EditableMovieList> MoviesProperty = RegisterProperty<EditableMovieList>(c => c.Movies);
        public EditableMovieList Movies
        {
            get { return GetProperty(MoviesProperty); }
            set { SetProperty(MoviesProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(FirstNameProperty, "You need to add a first name"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(FamilyNameProperty, "You need to add a family name"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(SexProperty, "You need to choose a gender"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(DescriptionProperty, "You need to add a description"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(FirstNameProperty, "You need to add a first name"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinValue<int>(AgeProperty, 1, "Please give a valid age"));
            BusinessRules.AddRule(new ValidName());
        }

        private class ValidName : Csla.Rules.BusinessRule
        {
            protected override void Execute(RuleContext context)
            {
                var actor = (EditableActor)context.Target;
                using(var contxt = new MovieDataLayer.DbContext.MovieDbContext())
                {
                    var alreadyExistingActors = contxt.Actor.Where(x => x.CharacterName == null).ToList();
                    foreach(var item in alreadyExistingActors)
                    {
                        var currentActorName = item.FamilyName + item.FirstName;
                        var actorToAddName = actor.FamilyName + actor.FirstName;
                        if (currentActorName.ToLower() == actorToAddName.ToLower())
                        {
                            context.AddErrorResult("There already exists an actor with the same name");
                        }
                    }
                }
            }
        }

        public static EditableActor NewActor(StakeholderCriteria criteria)
        {
            return DataPortal.Create<EditableActor>(criteria);
        }

        public static void DeleteActorById(int id)
        {
            DataPortal.Delete<EditableActor>(id);
        }

        public static EditableActor GetActorById(int id)
        {
            return DataPortal.Fetch<EditableActor>(id);
        }

        public static EditableActor ConvertChild(Actor actor)
        {
            return DataPortal.FetchChild<EditableActor>(actor);
        }
        
        protected void DataPortal_Create(StakeholderCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Age = criteria.Age;
                this.FamilyName = criteria.FamilyName;
                this.FirstName = criteria.FirstName;
                this.Sex = criteria.Sex;
                this.CountOscars = criteria.CountOscars;
                this.Description = criteria.Description;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                Actor actor = new Actor() {
                    Age = this.Age,
                    CountOscars = this.CountOscars,
                    Description = this.Description,
                    FamilyName = this.FamilyName,
                    FirstName = this.FirstName,
                    Sex = this.Sex
                };
                context.Actor.Add(actor);
                context.SaveChanges();
                this.Id = actor.Id;
            }
        }

        private void DataPortal_Delete(int id)
        {
            Actor actor;
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                actor = context.Actor.Where(x => x.Id == id).FirstOrDefault();
                if (actor == null)
                    throw new Exception("No actor was found");
                context.Actor.Remove(actor);
                context.SaveChanges();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                var actor = context.Actor.Where(x => x.Id == this.Id).FirstOrDefault();
                if (actor == null)
                    throw new Exception("No actor was found");
                actor.Age = this.Age;
                actor.CountOscars = this.CountOscars;
                actor.Description = this.Description;
                actor.FamilyName = this.FamilyName;
                actor.FirstName = this.FirstName;
                actor.Sex = this.Sex;
                context.SaveChanges();
            }
        }
        private void DataPortal_Fetch(int id)
        {
            Actor actor;
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                actor = context.Actor.Include("Movies").Where(x => x.Id == id).FirstOrDefault();
                if (actor == null)
                    throw new Exception("No actor was found");
            }
            this.Age = actor.Age;
            this.Id = actor.Id;
            this.CharacterName = actor.CharacterName;
            this.FamilyName = actor.FamilyName;
            this.FirstName = actor.FirstName;
            this.Sex = actor.Sex;
            this.CountOscars = actor.CountOscars;
            this.Description = actor.Description;
            this.Movies = EditableMovieList.ConvertChild(actor.Movies);
        }

        private void Child_Fetch(Actor actor)
        {
            this.Id = actor.Id;
            this.CharacterName = actor.CharacterName;
            this.FamilyName = actor.FamilyName;
            this.FirstName = actor.FirstName;
        }
    }
}
