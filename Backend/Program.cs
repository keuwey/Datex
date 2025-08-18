using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<UserDb>(opt => opt.UseInMemoryDatabase("UserList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/users", async (User user, UserDb db) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{user.Id}", user);
});

app.MapGet("/users", async (UserDb db) => await db.Users.ToListAsync());

app.MapGet("/users/{id:int}", async (int id, UserDb db) =>
    await db.Users.FindAsync(id) is User user ? Results.Ok(user) : Results.NotFound());

app.MapPut("/users/{id:int}", async (int id, User user, UserDb db) =>
{
    db.Users.Update(user);
    await db.SaveChangesAsync();
    return Results.Ok(user.Id);
});

app.MapPatch("/users/{id:int}", async (int id, UserDb db, Dictionary<string, object> updates) =>
{
    var user = await db.Users.FindAsync(id);
    if (user is null)
        return Results.NotFound();

    foreach (var entry in updates)
    {
        switch (entry.Key.ToLower())
        {
            case "name":
                user.Name = entry.Value?.ToString();
                break;
            case "phone":
                user.Phone = entry.Value?.ToString();
                break;
            case "password":
                user.Password = entry.Value?.ToString();
                break;
        }
    }

    await db.SaveChangesAsync();
    return Results.Ok(user);
});

app.MapDelete("/users/{id:int}", async (int id, UserDb db) =>
{
    var user = await db.Users.FindAsync(id);
    if (user is null)
        return Results.NotFound();

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();