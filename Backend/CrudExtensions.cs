using System;
using Microsoft.EntityFrameworkCore;
namespace Backend;

public static class CrudExtensions {
  public static void MapCrud<T>(this WebApplication app, string route) where T : class {
    app.MapGet($"/{route}", async (AppDb db) => await db.Set<T>().ToListAsync());

    app.MapGet($"/{route}/{{id:int}}", async (int id, AppDb db) => await db.Set<T>().FindAsync(id) is T entity ? Results.Ok(entity) : Results.NotFound());

    app.MapPost($"/{route}", async (T entity, AppDb db) => {
      db.Set<T>().Add(entity);
      await db.SaveChangesAsync();
      var idProp = typeof(T).GetProperty("Id")?.GetValue(entity);
      return Results.Created($"/{route}/{idProp}", entity);
    });

    app.MapPut($"/{route}/{{id:int}}", async (int id, T entity, AppDb db) => {
      var existing = await db.Set<T>().FindAsync(id);
      if (existing is null) return Results.NotFound();

      var idProp = typeof(T).GetProperty("Id");
      if (idProp is not null) idProp.SetValue(entity, id);

      db.Entry(existing).CurrentValues.SetValues(entity);
      await db.SaveChangesAsync();

      return Results.NoContent();
    });

    app.MapPatch($"/{route}/{{id:int}}", async (int id, AppDb db, Dictionary<string, object> updates) => {
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
      return Results.NoContent();
    });

    app.MapDelete($"/{route}/{{id:int}}", async (int id, AppDb db) => {
      var entity = await db.Set<T>().FindAsync(id);
      if (entity is null) return Results.NotFound();
      db.Set<T>().Remove(entity);
      await db.SaveChangesAsync();
      return Results.NoContent();
    });
  }
}