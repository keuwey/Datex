using System;
using System.Text.Json.Serialization;
namespace Backend;

// Para nomes em enums, sempre usar PascalCase e sem acentos, evitando assim problemas com serialização

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category {
  Hortifruti,
  AcougueFrios,
  PadariaConfeitaria,
  Bebidas,
  LaticiniosOvos,
  Mercearia,
  Congelados,
  Limpeza,
  Higiene_Beleza,
  UtilidadesDomesticas,
  BebesCrianças,
  PetShop,
  Calcados,
  Eletronicos,
  Livros
}