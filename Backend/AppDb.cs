using System;
using System.Collections.Generic;
using System.Linq;
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

    // Configurar relacionamento entre Sale e Client
    modelBuilder.Entity<Sale>()
      .HasOne(s => s.Client)
      .WithMany()
      .HasForeignKey(s => s.ClientId);

    modelBuilder.Entity<Sale>().Property(s => s.SaleStatus).HasConversion<string>();

    modelBuilder.Entity<Sale>().Property(s => s.PaymentMethod).HasConversion<string>();

    // Configurar TotalValue como propriedade ignorada pelo EF (calculada em tempo de execução)
    modelBuilder.Entity<Sale>().Ignore(s => s.TotalValue);
  }
}