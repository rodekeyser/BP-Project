using MovieDataLayer.DataModels;
using MovieDataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessLogicObjects
{
    public class BusinessActor
    {
        public static int CreateActor(string firstname, string familyname, string description, int countOscars, int age, Sex sex)
        {
            CheckActorParameters(firstname, familyname, description, age);
            Actor actorToAdd = new Actor();
            actorToAdd.FirstName = firstname;
            actorToAdd.FamilyName = familyname;
            actorToAdd.Description = description;
            actorToAdd.CountOscars = countOscars;
            actorToAdd.Age = age;
            actorToAdd.Sex = sex; 
            using(var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                context.Actor.Add(actorToAdd);
                context.SaveChanges();
            }
            return actorToAdd.Id;
        }

        public static List<Actor> GetAllActors()
        {
            List<Actor> actors;
            using(var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                actors = context.Actor.Where(x => x.CharacterName == null).ToList();
            }
            if (actors == null)
                throw new ValidationException("No actors were found", "None");
            return actors;
        }

        public static Actor GetActor(int id)
        {
            Actor actor;
            using(var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                actor = context.Actor.Include("Movies").Where(x => x.Id == id).FirstOrDefault();
                if (actor == null)
                    throw new ValidationException("No actor was found", "Id");
            }
            return actor;
        }

        public static void DeleteActor(int id)
        {
            using(var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                var actorToDelete = context.Actor.Find(id);
                if (actorToDelete == null)
                    throw new ValidationException("No actor was found to delete", "Id");
                context.Actor.Remove(actorToDelete);
                context.SaveChanges();
            }
        }

        public static Actor UpdateActor(int id, string firstname, string familyname, string description, int countOscars, int age, Sex sex)
        {
            CheckActorParameters(firstname, familyname, description, age);
            Actor actor;
            using (var context = new MovieDataLayer.DbContext.MovieDbContextNET())
            {
                actor = context.Actor.Find(id);
                if (actor == null)
                    throw new ValidationException("No actor was found", "Id");
                actor.FirstName = firstname;
                actor.FamilyName = familyname;
                actor.Description = description;
                actor.CountOscars = countOscars;
                actor.Age = age;
                actor.Sex = sex;
                context.SaveChanges();
            }
            return actor;
        }

        public static void CheckActorParameters(string firstname, string familyname, string description, int age)
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
                var allActors = context.Actor.ToList();
                foreach (var item in allActors)
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
