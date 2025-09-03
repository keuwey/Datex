using System;
using System.Text.Json.Serialization;
namespace Backend;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category
{
  Hortifrúti,
  Açougue_Frios,
  Padaria_Confeitaria,
  Bebidas,
  Laticínios_Ovos,
  Mercearia,
  Congelados,
  Limpeza,
  Higiene_Beleza,
  Utilidades_Domésticas,
  Bebês_Crianças,
  Pet_Shop,
  Calçados,
  Eletrônicos,
  Livros
}