using System;
using System.Collections.Generic;
using ECommerceMarket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ECommerceMarket.Data
{
    public partial class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext()
        {
        }

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Preset> Presets { get; set; } = null!;
        public virtual DbSet<PresetAttribute> PresetAttributes { get; set; } = null!;
        public virtual DbSet<PricesArchive> PricesArchives { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LEGA\\SQLEXPRESS;Database=ECommerceDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("Order_Products");

                entity.HasIndex(e => e.OrderId, "IX_Order_Products_OrderId");

                entity.HasIndex(e => e.ProductId, "IX_Order_Products_ProductId");

                entity.Property(e => e.OrderedPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Preset>(entity =>
            {
                entity.HasIndex(e => e.SubCategoryId, "IX_Presets_SubCategoryId");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Presets)
                    .HasForeignKey(d => d.SubCategoryId);
            });

            modelBuilder.Entity<PresetAttribute>(entity =>
            {
                entity.ToTable("Preset_Attributes");

                entity.HasIndex(e => e.AttributeId, "IX_Preset_Attributes_AttributeId");

                entity.HasIndex(e => e.PresetId, "IX_Preset_Attributes_PresetId");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.PresetAttributes)
                    .HasForeignKey(d => d.AttributeId);

                entity.HasOne(d => d.Preset)
                    .WithMany(p => p.PresetAttributes)
                    .HasForeignKey(d => d.PresetId);
            });

            modelBuilder.Entity<PricesArchive>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_PricesArchives_ProductId");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PricesArchives)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.DiscountId, "IX_Products_DiscountId");

                entity.HasIndex(e => e.PresetId, "IX_Products_PresetId");

                entity.Property(e => e.Name).HasDefaultValueSql("(N'')");

                entity.Property(e => e.PhotosJson)
                    .HasColumnName("PhotosJSON")
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.DiscountId);

                entity.HasOne(d => d.Preset)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PresetId);
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.ToTable("Product_Attributes");

                entity.HasIndex(e => e.AttributeId, "IX_Product_Attributes_AttributeId");

                entity.HasIndex(e => e.ProductId, "IX_Product_Attributes_ProductId");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.ProductAttributes)
                    .HasForeignKey(d => d.AttributeId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductAttributes)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_SubCategories_CategoryId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
