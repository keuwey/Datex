using System;
using Microsoft.EntityFrameworkCore;
namespace Backend;

public class AppDb : DbContext {
  public AppDb(DbContextOptions<AppDb> options) : base(options) { }
  public DbSet<User> Users => Set<User>();
  public DbSet<Client> Clients => Set<Client>();
  public DbSet<Product> Products => Set<Product>();

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<Product>().Property(p => p.ProductCategory).HasConversion<string>();
  }
}