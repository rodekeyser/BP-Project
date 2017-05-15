using MovieDataLayer.DataModels;
using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessLogicObjects
{
    public class BusinessDirector
    {
        public static int CreateDirector(string firstname, string familyname, string description, int countOscars, int age, Sex sex)
        {
            CheckDirectorParameters(firstname, familyname, description, age);

            Director directorToAdd = new Director();
            directorToAdd.FirstName = firstname;
            directorToAdd.FamilyName = familyname;
            directorToAdd.Description = description;
            directorToAdd.CountOscars = countOscars;
            directorToAdd.Age = age;
            directorToAdd.Sex = sex;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                context.Director.Add(directorToAdd);
                context.SaveChanges();
            }
            return directorToAdd.Id;
        }

        public static List<Director> GetAllDirectors()
        {
            List<Director> directors;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                directors = context.Director.ToList();
            }
            if (directors == null)
                throw new ValidationException("No directors were found", "None");
            return directors;
        }

        public static Director GetDirector(int id)
        {
            Director director;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                director = context.Director.Include("Movies").Where(x => x.Id == id).FirstOrDefault();
                if (director == null)
                    throw new ValidationException("No director was found", "Id");
            }
            return director;
        }

        public static void DeleteDirector(int id)
        {
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                var directorToDelete = context.Director.Find(id);
                if (directorToDelete == null)
                    throw new ValidationException("No director was found to delete", "Id");
                context.Director.Remove(directorToDelete);
                context.SaveChanges();
            }
        }

        public static Director UpdateDirector(int id, string firstname, string familyname, string description, int countOscars, int age, Sex sex)
        {
            CheckDirectorParameters(firstname, familyname, description, age);
            Director director;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                director = context.Director.Find(id);
                if (director == null)
                    throw new ValidationException("No director was found", "Id");
                director.FirstName = firstname;
                director.FamilyName = familyname;
                director.Description = description;
                director.CountOscars = countOscars;
                director.Age = age;
                director.Sex = sex;
                context.SaveChanges();
            }
            return director;
        }
        
        public static void CheckDirectorParameters(string firstname, string familyname, string description, int age)
        {
            if (firstname == null)
                throw new ValidationException("You need to add a first name.", "FirstName");

            if (familyname == null)
                throw new ValidationException("You need to add a family name.", "FamilyName");

            if (description == null)
                throw new ValidationException("You need to add a description.", "Description");

            if (age < 1)
                throw new ValidationException("Please give a valid age", "Age");
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                var allDirectors = context.Director.ToList();
                foreach (var item in allDirectors)
                {
                    var itemName = item.FirstName.ToLower() + item.FamilyName.ToLower();
                    var actorName = firstname.ToLower() + familyname.ToLower();
                    if (itemName == actorName)
                        throw new ValidationException("This actor already exists", "FirstName");
                }
            }
        }
    }
}
