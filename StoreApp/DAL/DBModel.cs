using System.Data.Entity;

namespace StoreApp.DAL
{
    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Space> Spaces { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Space>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Space)
                .HasForeignKey(e => e.SpaceFK);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Spaces)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.StoreFK)
                .WillCascadeOnDelete(false);
        }
    }
}
