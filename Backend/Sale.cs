using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend;


public class Sale {
  public uint Id { get; private set; }
  public uint ClientId { get; set; } // TODO: Solve the shadowing problem with EF. It seems that "ClientId" is already being used somewhere
  public Client? Client { get; set; }
  public DateTime DateHour { get; set; } = DateTime.Now;
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // TODO: search more about computed values. It seems to be related to my problem
  public List<SaleItem> Items { get; set; } = new(); // TODO: not working
  public decimal TotalValue => Items?.Sum(i => i.Subtotal) ?? 0; // not working
  public PaymentMethod PaymentMethod { get; set; }
  public string? Notes { get; set; }
  public Status SaleStatus { get; set; } // Showing as "null" on sqlite
}
