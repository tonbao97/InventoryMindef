namespace Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class InventoryEntities : IdentityDbContext<ApplicationUser>
    {
        // Your context has been configured to use a 'InventoryEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Data.InventoryEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'InventoryEntities' 
        // connection string in the application configuration file.
        public InventoryEntities()
            : base("name=InventoryEntities")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ComputerDetails> ComputerDetailss { get; set; }
        public DbSet<CPUOption> CPUOptions { get; set; }
        public DbSet<DeliveryPackage> DeliveryPackages { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<HDDOption> HDDOptions { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<MainUnit> MainUnits { get; set; }
        public DbSet<MovementLog> MovementLogs { get; set; }
        public DbSet<OSOption> OSOptions { get; set; }
        public DbSet<Ownership> Ownerships { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<RamOption> RamOptions { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<SubUnit> SubUnits { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<VGAOption> VGAOptions { get; set; }
        public DbSet<WebsiteAttribute> WebsiteAttributes { get; set; }
        public DbSet<Logging> Loggings { get; set; }



        public static InventoryEntities Create()
        {
            return new InventoryEntities();
        }

        public virtual void Commit()
        {
            try
            {

                base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

        }

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