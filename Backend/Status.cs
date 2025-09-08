using System;
using System.Text.Json.Serialization;
namespace Backend;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status {
  Completed,
  Pending,
  Cancelled
}