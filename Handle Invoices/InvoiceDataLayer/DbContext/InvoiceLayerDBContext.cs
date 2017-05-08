namespace InvoiceDataLayer.DbContext
{
    using DataModels;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class InvoiceLayerDBContext : DbContext
    {
        // Your context has been configured to use a 'InvoiceLayerDBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'InvoiceDataLayer.DbContext.InvoiceLayerDBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'InvoiceLayerDBContext' 
        // connection string in the application configuration file.
        public InvoiceLayerDBContext()
            : base("name=InvoiceLayerDBContext")
        {

        }

        public virtual DbSet<InvoiceHeader> InvoiceHeader { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLine { get; set; }
        public virtual DbSet<InvoiceException> InvoiceException { get; set; }
        public virtual DbSet<InvoiceNumber> InvoiceNumber { get; set; }

        
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