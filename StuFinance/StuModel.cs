using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StuFinance
{
    public partial class StuModel : DbContext
    {
        public StuModel()
            : base("name=StuModelString")
        {
        }

        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<Incom> Incoms { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Family>()
                .HasMany(e => e.Costs)
                .WithRequired(e => e.Family)
                .HasForeignKey(e => e.id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Family>()
                .HasMany(e => e.Incoms)
                .WithRequired(e => e.Family)
                .HasForeignKey(e => e.id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Family>()
                .HasMany(e => e.Receipts)
                .WithRequired(e => e.Family)
                .HasForeignKey(e => e.id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Family>()
                .HasMany(e => e.Transfers)
                .WithRequired(e => e.Family)
                .HasForeignKey(e => e.id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cost>()
                .Property(e => e.sum_cost)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Incom>()
                .Property(e => e.sum_incom)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Receipt>()
                .Property(e => e.image_receipt)
                .IsUnicode(false);

            modelBuilder.Entity<Transfer>()
                .Property(e => e.sum_transfer)
                .HasPrecision(12, 2);
        }
    }
}
