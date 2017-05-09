namespace UserDataLayer.DbContext
{
    using DataModels;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserDbContext : DbContext
    {
        // Your context has been configured to use a 'UserDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'UserDataLayer.DbContext.UserDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'UserDbContext' 
        // connection string in the application configuration file.
        public UserDbContext()
            : base("name=UserDbContext")
        {
            
        }
        public virtual DbSet<Achievement> Achievement { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Watchlist> Watchlist { get; set; }
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