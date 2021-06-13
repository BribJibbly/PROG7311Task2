using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OrdersAppTask2.Models
{
    public partial class Prog7311task2Context : DbContext
    {
        public Prog7311task2Context()
        {
        }

        public Prog7311task2Context(DbContextOptions<Prog7311task2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("InformationDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("orderId");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("productName");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProductNameNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductName)
                    .HasConstraintName("FK__Orders__productN__3F466844");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__Orders__Username__3E52440B");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductName)
                    .HasName("PK__Products__0A9CBBE1E9301760");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("productName");

                entity.Property(e => e.ProductCategory)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("productCategory");

                entity.Property(e => e.ProductPrice).HasColumnName("productPrice");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__536C85E5A645000C");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
