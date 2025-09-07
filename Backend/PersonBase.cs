using System;
namespace Backend;

public abstract class PersonBase {
  public uint Id { get; private set; }
  public string? Name { get; set; }
  public string? Phone { get; set; }
}