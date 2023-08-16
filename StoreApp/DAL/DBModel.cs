using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

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
                .WithRequired(e => e.Space)
                .HasForeignKey(e => e.SpaceFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Spaces)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.StoreFK)
                .WillCascadeOnDelete(false);
        }
    }
}
