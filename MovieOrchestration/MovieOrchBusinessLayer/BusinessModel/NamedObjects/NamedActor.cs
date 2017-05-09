using Csla;
using MovieOrchBusinessLayer.BusinessModel.CriteriaObjects;
using MovieOrchBusinessLayer.BusinessModel.NamedLists;
using MovieOrchBusinessLayer.BusinessModel.ReadOnlyLists;
using MovieOrchBusinessLayer.MovieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrchBusinessLayer.BusinessModel.NamedObjects
{
    [Serializable]
    public class NamedActor: BusinessBase<NamedActor>
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
        public static readonly PropertyInfo<NamedMovieList> MoviesProperty = RegisterProperty<NamedMovieList>(c => c.Movies);
        public NamedMovieList Movies
        {
            get { return GetProperty(MoviesProperty); }
            set { SetProperty(MoviesProperty, value); }
        }
        public static readonly PropertyInfo<NamedExceptionReadOnlyList> ExceptionsProperty = RegisterProperty<NamedExceptionReadOnlyList>(c => c.Exceptions);
        public NamedExceptionReadOnlyList Exceptions
        {
            get { return GetProperty(ExceptionsProperty); }
            set { SetProperty(ExceptionsProperty, value); }
        }

        public static NamedActor NewNamedActor(StakeholderCriteria criteria)
        {
            return DataPortal.Create<NamedActor>(criteria);
        }

        public static NamedActor ConvertChild(ChildStakeholderResponse response)
        {
            return DataPortal.FetchChild<NamedActor>(response);
        }

        public static NamedActor GetActorById(int id)
        {
            return DataPortal.Fetch<NamedActor>(id);
        }

        public static void DeleteActorById(int id)
        {
            DataPortal.Delete<NamedActor>(id);
        }

        protected void DataPortal_Create(StakeholderCriteria criteria)
        {
            using (BypassPropertyChecks)
            {
                this.Age = criteria.Age;
                this.CountOscars = criteria.CountOscars;
                this.Description = criteria.Description;
                this.FamilyName = criteria.FamilyName;
                this.FirstName = criteria.FirstName;
                this.Sex = criteria.Sex;
            }
            base.DataPortal_Create();
        }

        protected override void DataPortal_Insert()
        {
            StakeholderInput actor = new StakeholderInput();
            actor.Age = this.Age;
            actor.CountOscars = this.CountOscars;
            actor.Description = this.Description;
            actor.FamilyName = this.FamilyName;
            actor.FirstName = this.FirstName;
            actor.Sex = this.Sex;
            actor.Application = "movie";
            actor.Password = "movie";
            IdentificationResponse response;
            using (var service = new MovieServiceClient())
            {
                response = service.CreateActor(actor);
                this.Id = response.Id;
            }
            if (!response.Succes)
            {
                this.Exceptions = NamedExceptionReadOnlyList.ConvertErrorDataObject(response.Errors.ToList());
                return;
            }
        }

        private void DataPortal_Fetch(int id)
        {
            StakeholderResponse response;
            using (var service = new MovieServiceClient())
            {
                response = service.GetActorById(new IdentificationInput()
                {
                    Id = id,
                    Application = "movie",
                    Password = "movie"
                });
            }
            if (!response.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in response.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
            else
            {
                this.Age = response.Age;
                this.Id = response.Id;
                this.FamilyName = response.FamilyName;
                this.FirstName = response.FirstName;
                this.Sex = response.Sex;
                this.CountOscars = response.CountOscars;
                this.Description = response.Description;
                if (response.Movies != null && response.Movies.ToList().Count > 0)
                this.Movies = NamedMovieList.ConvertChild(response.Movies.ToList());
            }
        }

        private void Child_Fetch(ChildStakeholderResponse response)
        {
            this.Id = response.Id;
            this.CharacterName = response.CharacterName;
            this.FamilyName = response.FamilyName;
            this.FirstName = response.FirstName;
            MarkAsChild();
        }

        private void DataPortal_Delete(int id)
        {
            BaseResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.DeleteActorById(new IdentificationInput()
                {
                    Id = id,
                    Application = "movie",
                    Password = "movie"
                });
            }
            if (!response.Succes)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in response.Errors)
                {
                    message.Append(item.ErrorMessage);

                }
                throw new Exception(message.ToString());
            }
        }

        protected override void DataPortal_Update()
        {
            StakeholderResponse response;
            using(var service = new MovieServiceClient())
            {
                response = service.UpdateActor(new StakeholderInput()
                {
                    Id = this.Id,
                    Age = this.Age,
                    CountOscars = this.CountOscars,
                    Description = this.Description,
                    FamilyName = this.FamilyName,
                    FirstName = this.FirstName,
                    Sex = this.Sex,
                    Application = "movie",
                    Password = "movie"
                });
            }
            if (!response.Succes)
            {
                this.Exceptions = NamedExceptionReadOnlyList.ConvertErrorDataObject(response.Errors.ToList());
                return;
            }
            else
            {
                this.Age = response.Age;
                this.Id = response.Id;
                this.FamilyName = response.FamilyName;
                this.FirstName = response.FirstName;
                this.Sex = response.Sex;
                this.CountOscars = response.CountOscars;
                this.Description = response.Description;
            }
        }
    }
}
