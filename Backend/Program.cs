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

app.MapPost("/auth/register", async (User user, UserDb db) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/auth/register/{user.Id}", user);
});

app.MapGet("/auth/users", async (UserDb db) => await db.Users.ToListAsync());

app.MapGet("/auth/users/{id}", async (int id, UserDb db) =>
    await db.Users.FindAsync(id) is User user ? Results.Ok(user) : Results.NotFound());

app.MapPut("/auth/users/{id}", async (int id, User user, UserDb db) =>
{
    db.Users.Update(user);
    await db.SaveChangesAsync();
});

app.MapPatch("/auth/users/{id}", async (int id, UserDb db, Dictionary<string, object> updates) =>
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

app.MapDelete("/auth/users/{id}", async (int id, UserDb db) =>
{
    var user = await db.Users.FindAsync(id);
    if (user is null)
        return Results.NotFound();

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();