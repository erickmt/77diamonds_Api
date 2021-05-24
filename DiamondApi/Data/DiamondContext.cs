namespace DiamondApi.Data
{
    using DiamondApi.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public partial class DiamondContext : DbContext
    {
        public DiamondContext()
            : base("name=DiamondContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<ItemPhotoPropertySet> ItemPhotoPropertySet { get; set; }
        public virtual DbSet<ItemPhotos> ItemPhotos { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<TypePropertySet> TypePropertySet { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        
        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Deleted &&
                        e.Entity.GetType().GetProperties().Any(x => x.Name == "IsActive")))
            {
                item.State = EntityState.Unchanged;
                item.CurrentValues["IsActive"] = false;
                item.CurrentValues["ModifiedAt"] = DateTime.Now;
                item.Property("CreatedAt").IsModified = false;
            }

            foreach (var item in ChangeTracker.Entries()
               .Where(e => e.State != EntityState.Unchanged &&
               e.Entity.GetType().GetProperties().Any(x => x.Name == "ModifiedAt")))
            {
                item.CurrentValues["ModifiedAt"] = DateTime.Now;
                item.Property("CreatedAt").IsModified = false;
            }

            foreach (var item in ChangeTracker.Entries()
              .Where(e => e.State == EntityState.Added &&
              e.Entity.GetType().GetProperties().Any(x => x.Name == "CreatedAt")))
            {
                item.CurrentValues["CreatedAt"] = DateTime.Now;
                item.CurrentValues["IsActive"] = true;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemPhotos>()
                .HasMany(e => e.ItemPhotoPropertySet)
                .WithRequired(e => e.ItemPhotos)
                .HasForeignKey(e => e.ItemPhotoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Items>()
                .HasMany(e => e.ItemPhotos)
                .WithOptional(e => e.Items)
                .HasForeignKey(e => e.ItemId);

            modelBuilder.Entity<Properties>()
                .HasMany(e => e.ItemPhotoPropertySet)
                .WithRequired(e => e.Properties)
                .HasForeignKey(e => e.PropertyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Properties>()
                .HasMany(e => e.TypePropertySet)
                .WithRequired(e => e.Properties)
                .HasForeignKey(e => e.PropertyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Types>()
                .HasMany(e => e.ItemPhotos)
                .WithRequired(e => e.Types)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Types>()
                .HasMany(e => e.TypePropertySet)
                .WithRequired(e => e.Types)
                .HasForeignKey(e => e.MediaTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
