using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Backend;


public class Sale {
  public uint Id { get; private set; }
  public uint ClientId { get; set; }
  public Client? Client { get; set; }
  public DateTime DateHour { get; set; } = DateTime.Now;
  public List<SaleItem> Items { get; set; } = new();
  public decimal TotalValue => Items?.Sum(i => i.Subtotal) ?? 0;
  public PaymentMethod PaymentMethod { get; set; }
  public string? Notes { get; set; }
  public Status SaleStatus { get; set; }
}