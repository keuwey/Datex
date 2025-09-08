using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
namespace Backend;

class Program {
  static void Main(string[] args) {
    var builder = WebApplication.CreateBuilder(args);
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=AppDb.db";
    builder.Services.AddOpenApi();
    builder.Services.AddDbContext<AppDb>(opt => opt.UseSqlite(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    var app = builder.Build();

    if (app.Environment.IsDevelopment()) app.MapOpenApi();

    app.UseHttpsRedirection();
    app.MapCrud<User>("users");
    app.MapCrud<Client>("clients");
    app.MapCrud<Product>("products");
    app.MapCrud<Sale>("sales");
    app.Run();
  }
}