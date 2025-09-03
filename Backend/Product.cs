using System;
namespace Backend;

public class Product {
  public int Id { get; set; }
  public string? Name { get; set; }
  public int? Sku { get; set; } // Stock Keeping Unit. Numbers with 9 digits
  public string? Description { get; set; }
  public string? Brand { get; set; }
  public decimal? Price { get; set; }
  public int? StockQuantity { get; set; }
  public int? MinimumStock { get; set; }
  public DateTime? CreatedAt { get; set; } = DateTime.Now;
  public DateTime? UpdatedAt { get; set; } = DateTime.Now;
  public Category? ProductCategory { get; set; }
  public string? UrlImagem { get; set; }
  public bool Active { get; set; } = true;
}