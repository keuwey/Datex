using System;
using Microsoft.EntityFrameworkCore;
namespace Backend;

class Program {
  static void Main(string[] args) {
    var builder = WebApplication.CreateBuilder(args);
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=AppDb.db";
    builder.Services.AddOpenApi();
    builder.Services.AddDbContext<AppDb>(opt => opt.UseSqlite(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    var app = builder.Build();

    if (app.Environment.IsDevelopment()) app.MapOpenApi();

    app.UseHttpsRedirection();
    app.MapCrud<User>("users");
    app.MapCrud<Client>("clients");
    app.Run();

  }
}