public static class CrudExtensions
{
    public static void MapCrud<T>(this WebApplication app, string route) where T : class
    {
        app.MapPost($"/{route}", async (T entity, AppDb db) =>
        {
            db.Set<T>().Add(entity);
            await db.SaveChangesAsync();
            var idProp = typeof(T).GetProperty("Id")?.GetValue(entity);
            return Results.Created($"/{route}/{idProp}", entity);
        });

        app.MapGet($"/{route}", async (AppDb db) =>
            await db.Set<T>().ToListAsync());

        app.MapGet($"/{route}/{{id:int}}", async (int id, AppDb db) =>
            await db.Set<T>().FindAsync(id) is T entity ? Results.Ok(entity) : Results.NotFound());

        app.MapPut($"/{route}/{{id:int}}", async (int id, T entity, AppDb db) =>
        {
            db.Set<T>().Update(entity);
            await db.SaveChangesAsync();
            return Results.Ok(id);
        });

        app.MapDelete($"/{route}/{{id:int}}", async (int id, AppDb db) =>
        {
            var entity = await db.Set<T>().FindAsync(id);
            if (entity is null)
                return Results.NotFound();

            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
