using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Backend;


public class SaleItem {
  public uint Id { get; private set; }
  public uint SaleId { get; set; }
  public Sale? Sale { get; set; }
  public uint ProductId { get; set; }
  public Product? Product { get; set; }
  public uint Quantity { get; set; }
  public decimal UnityPrice { get; set; }
  public decimal Subtotal => Quantity * UnityPrice;
}