using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace Backend;

public class AppDb : DbContext {
  public AppDb(DbContextOptions<AppDb> options) : base(options) { }
  public DbSet<User> Users => Set<User>();
  public DbSet<Client> Clients => Set<Client>();
  public DbSet<Product> Products => Set<Product>();
  public DbSet<Sale> Sales => Set<Sale>();
  public DbSet<SaleItem> SaleItems => Set<SaleItem>();
  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<Product>().Property(p => p.ProductCategory).HasConversion<string>();
    
    modelBuilder.Entity<Sale>()
      .HasMany(s => s.Items)
      .WithOne(i => i.Sale)
      .HasForeignKey(i => i.SaleId)
      .OnDelete(DeleteBehavior.Cascade);
    
    modelBuilder.Entity<Sale>()
      .Property(s => s.SaleStatus)
      .HasConversion<string>();

    modelBuilder.Entity<Sale>()
      .Property(s => s.PaymentMethod)
      .HasConversion<string>();
  }
}