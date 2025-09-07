using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
namespace Backend;

public static class CrudExtensions {
  public static void MapCrud<T>(this WebApplication app, string route) where T : class {
    app.MapGet($"/{route}", async (AppDb db) => await db.Set<T>().ToListAsync());

    app.MapGet($"/{route}/{{id:int}}", async (uint id, AppDb db) => await db.Set<T>().FindAsync(id) is T entity ? Results.Ok(entity) : Results.NotFound());

    app.MapPost($"/{route}", async (T entity, AppDb db) => {
      db.Set<T>().Add(entity);
      await db.SaveChangesAsync();
      if (entity is Sale sale) {
        var saved = await db.Sales
          .Include(s => s.Items)
          .FirstOrDefaultAsync(s => s.Id == sale.Id);
        return Results.Created($"/sales/{sale.Id}", saved);
      }
      var idProp = typeof(T).GetProperty("Id")?.GetValue(entity);
      return Results.Created($"/{route}/{idProp}", entity);
    });

    app.MapPut($"/{route}/{{id:int}}", async (uint id, T entity, AppDb db) => {
      var existing = await db.Set<T>().FindAsync(id);
      if (existing is null) return Results.NotFound();

      var idProp = typeof(T).GetProperty("Id");
      if (idProp is not null) idProp.SetValue(entity, id);

      db.Entry(existing).CurrentValues.SetValues(entity);
      await db.SaveChangesAsync();

      return Results.NoContent();
    });

    app.MapPatch($"/{route}/{{id:int}}", async (uint id, AppDb db, Dictionary<string, object> updates) => {
      var entity = await db.Set<T>().FindAsync(id);
      if (entity is null) return Results.NotFound();

      if (entity is User user) {
        foreach (var entry in updates) {
          switch (entry.Key.ToLower()) {
            case "name":
              user.Name = entry.Value?.ToString();
              break;
            case "phone":
              user.Phone = entry.Value?.ToString();
              break;
            case "email":
              user.Email = entry.Value?.ToString();
              break;
            case "password":
              user.Password = entry.Value?.ToString();
              break;
          }
        }

        await db.SaveChangesAsync();
        return Results.Ok(user);
      }

      if (entity is Client client) {
        foreach (var entry in updates) {
          switch (entry.Key.ToLower()) {
            case "name":
              client.Name = entry.Value?.ToString();
              break;
            case "phone":
              client.Phone = entry.Value?.ToString();
              break;
          }
        }

        await db.SaveChangesAsync();
        return Results.Ok(client);
      }

      if (entity is Product product) {
        foreach (var entry in updates) {
          switch (entry.Key.ToLower()) {
            case "name":
              product.Name = entry.Value?.ToString();
              break;
            case "sku":
              if (entry.Value is JsonElement skuElem && skuElem.ValueKind == JsonValueKind.Number) product.Sku = skuElem.GetUInt32();
              break;
            case "description":
              product.Description = entry.Value?.ToString();
              break;
            case "brand":
              product.Brand = entry.Value?.ToString();
              break;
            case "price":
              if (entry.Value is JsonElement priceElem && priceElem.ValueKind == JsonValueKind.Number) product.Price = priceElem.GetDecimal();
              break;
            case "stockquantity":
              if (entry.Value is JsonElement stockElem && stockElem.ValueKind == JsonValueKind.Number) product.StockQuantity = stockElem.GetUInt32();
              break;
            case "minimumstock":
              if (entry.Value is JsonElement minStockElem && minStockElem.ValueKind == JsonValueKind.Number) product.MinimumStock = minStockElem.GetUInt32();
              break;
            case "productcategory":
              if (entry.Value is JsonElement catElem && catElem.ValueKind == JsonValueKind.String) {
                var categoryStr = catElem.GetString();
                if (Enum.TryParse<Category>(categoryStr, ignoreCase: true, out var parsedCategory)) product.ProductCategory = parsedCategory;
              }
              break;
            case "urlimage":
              product.UrlImage = entry.Value?.ToString();
              break;
            case "active":
              if (entry.Value is JsonElement activeElem && activeElem.ValueKind == JsonValueKind.True) product.Active = true;
              else if (entry.Value is JsonElement activeElem2 && activeElem2.ValueKind == JsonValueKind.False) product.Active = false;
              break;
          }
        }
        await db.SaveChangesAsync();
        return Results.Ok(product);
      }
      return Results.NoContent();
    });

    app.MapDelete($"/{route}/{{id:int}}", async (uint id, AppDb db) => {
      var entity = await db.Set<T>().FindAsync(id);
      if (entity is null) return Results.NotFound();
      db.Set<T>().Remove(entity);
      await db.SaveChangesAsync();
      return Results.NoContent();
    });
  }
}