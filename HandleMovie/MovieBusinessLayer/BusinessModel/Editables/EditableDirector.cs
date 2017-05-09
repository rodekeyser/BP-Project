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
    public class EditableDirector: BusinessBase<EditableDirector>
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
        }

        private class ValidName : Csla.Rules.BusinessRule
        {
            protected override void Execute(RuleContext context)
            {
                var director = (EditableDirector)context.Target;
                using (var contxt = new MovieDataLayer.DbContext.MovieDbContext())
                {
                    var alreadyExistingDirectors = contxt.Director.ToList();
                    foreach (var item in alreadyExistingDirectors)
                    {
                        var currentDirectorName = item.FamilyName + item.FirstName;
                        var directorToAddName = director.FamilyName + director.FirstName;
                        if (currentDirectorName.ToLower() == directorToAddName.ToLower())
                        {
                            context.AddErrorResult("There already exists a director with the same name");
                        }
                    }
                }
            }
        }

        public static EditableDirector NewDirector(StakeholderCriteria criteria)
        {
            return DataPortal.Create<EditableDirector>(criteria);
        }

        public static void DeleteDirectorById(int id)
        {
            DataPortal.Delete<EditableDirector>(id);
        }

        public static EditableDirector GetDirectorById(int id)
        {
            return DataPortal.Fetch<EditableDirector>(id);
        }

        public static EditableDirector ConvertChild(Director director)
        {
            return DataPortal.FetchChild<EditableDirector>(director);
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
                Director director = new Director()
                {
                    Age = this.Age,
                    CountOscars = this.CountOscars,
                    Description = this.Description,
                    FamilyName = this.FamilyName,
                    FirstName = this.FirstName,
                    Sex = this.Sex
                };
                context.Director.Add(director);
                context.SaveChanges();
                this.Id = director.Id;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                var director = context.Director.Where(x => x.Id == this.Id).FirstOrDefault();
                if (director == null)
                    throw new Exception("No actor was found");
                director.Age = this.Age;
                director.CountOscars = this.CountOscars;
                director.Description = this.Description;
                director.FamilyName = this.FamilyName;
                director.FirstName = this.FirstName;
                director.Sex = this.Sex;
                context.SaveChanges();
            }
        }

        private void DataPortal_Delete(int id)
        {
            Director director;
            using (var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                director = context.Director.Where(x => x.Id == id).FirstOrDefault();
                if (director == null)
                    throw new Exception("No director was found");
                context.Director.Remove(director);
                context.SaveChanges();
            }
        }
        private void DataPortal_Fetch(int id)
        {
            Director director;
            using(var context = new MovieDataLayer.DbContext.MovieDbContext())
            {
                director = context.Director.Include("Movies").Where(x => x.Id == id).FirstOrDefault();
            }
            if (director == null)
                throw new Exception("No director was found");
            this.Id = director.Id;
            this.Age = director.Age;
            this.FamilyName = director.FamilyName;
            this.FirstName = director.FirstName;
            this.CountOscars = director.CountOscars;
            this.Description = director.Description;
            this.Sex = director.Sex;
            this.Movies = EditableMovieList.ConvertChild(director.Movies);
        }

        private void Child_Fetch(Director director)
        {
            this.Id = director.Id;
            this.FamilyName = director.FamilyName;
            this.FirstName = director.FirstName;
            MarkAsChild();
        }
    }
}
