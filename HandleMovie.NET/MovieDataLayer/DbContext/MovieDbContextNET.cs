namespace MovieDataLayer.DbContext
{
    using DataModels;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MovieDbContextNET : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MovieDataLayer.DbContext.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public MovieDbContextNET()
            : base("name=MovieDbContextNET")
        {
        }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Stakeholder> Stakeholder { get; set; }
        public virtual DbSet<WatchList> Watchlist { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}