using System;
using System.Collections.Generic;
using API.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public partial class PwtDbContext : DbContext
{
    public PwtDbContext(){}

    public PwtDbContext(DbContextOptions<PwtDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beholdning> Beholdning { get; set; }

    public virtual DbSet<ProductData> ProductData { get; set; }

    public virtual DbSet<ShopInventory> ShopInventories { get; set; }

    public virtual DbSet<Varer> Varer { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beholdning>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Beholdning");

            entity.Property(e => e.ean)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductData>(entity =>
        {
            entity.HasKey(e => e.IDENT).HasName("KPH_identkey");

            entity.Property(e => e.FRADATO).HasColumnType("datetime");
            entity.Property(e => e.KAEDE)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Kampagnenavn)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.TILDATO).HasColumnType("datetime");
            entity.Property(e => e.Valutakode)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ShopInventory>(entity =>
        {
            entity.HasKey(e => e.IDENT).HasName("KPL_identkey");

            entity.ToTable("ShopInventory");

            entity.HasIndex(e => new { e.HOVED_Ident, e.VARENUMMER }, "KPL_varenummerkey").IsUnique();

            entity.Property(e => e.PRIS).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.VARENUMMER)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Varer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Varer");

            entity.Property(e => e.ColorCodeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CostPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CostPriceCurrency)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EAN)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ItemGroupName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Season)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StyleNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SuggestedRetailPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.URL)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.length)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }
   

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
