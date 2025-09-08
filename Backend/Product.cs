using System;
namespace Backend;

public class Product {
  public uint Id { get; private set; }
  public string? Name { get; set; }
  public uint? Sku { get; set; } // Stock Keeping Unit. Numbers with 10 digits
  public string? Description { get; set; }
  public string? Brand { get; set; }
  public decimal? Price { get; set; }
  public uint? StockQuantity { get; set; }
  public uint? MinimumStock { get; set; }
  public DateTime? CreatedAt { get; set; } = DateTime.Now;
  public DateTime? UpdatedAt { get; set; } = DateTime.Now;
  public Category? ProductCategory { get; set; }
  public string? UrlImage { get; set; }
  public bool Active { get; set; } = true;
}