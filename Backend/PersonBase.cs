using System;
namespace Backend;

public abstract class PersonBase {
  public int Id { get; private set; }
  public string? Name { get; set; }
  public string? Phone { get; set; }
}