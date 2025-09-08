using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations {
  /// <inheritdoc />
  public partial class InitialCreate : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.CreateTable(
          name: "Clients",
          columns: table => new {
            Id = table.Column<uint>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: true),
            Phone = table.Column<string>(type: "TEXT", nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Clients", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Products",
          columns: table => new {
            Id = table.Column<uint>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: true),
            Sku = table.Column<uint>(type: "INTEGER", nullable: true),
            Description = table.Column<string>(type: "TEXT", nullable: true),
            Brand = table.Column<string>(type: "TEXT", nullable: true),
            Price = table.Column<decimal>(type: "TEXT", nullable: true),
            StockQuantity = table.Column<uint>(type: "INTEGER", nullable: true),
            MinimumStock = table.Column<uint>(type: "INTEGER", nullable: true),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
            UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
            ProductCategory = table.Column<string>(type: "TEXT", nullable: true),
            UrlImage = table.Column<string>(type: "TEXT", nullable: true),
            Active = table.Column<bool>(type: "INTEGER", nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Products", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new {
            Id = table.Column<uint>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Email = table.Column<string>(type: "TEXT", nullable: true),
            Password = table.Column<string>(type: "TEXT", nullable: true),
            Name = table.Column<string>(type: "TEXT", nullable: true),
            Phone = table.Column<string>(type: "TEXT", nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Users", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Sales",
          columns: table => new {
            Id = table.Column<uint>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            ClientId = table.Column<uint>(type: "INTEGER", nullable: false),
            DateHour = table.Column<DateTime>(type: "TEXT", nullable: false),
            PaymentMethod = table.Column<string>(type: "TEXT", nullable: false),
            Notes = table.Column<string>(type: "TEXT", nullable: true),
            SaleStatus = table.Column<string>(type: "TEXT", nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Sales", x => x.Id);
            table.ForeignKey(
                      name: "FK_Sales_Clients_ClientId",
                      column: x => x.ClientId,
                      principalTable: "Clients",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "SaleItems",
          columns: table => new {
            Id = table.Column<uint>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            SaleId = table.Column<uint>(type: "INTEGER", nullable: false),
            ProductId = table.Column<uint>(type: "INTEGER", nullable: false),
            Quantity = table.Column<uint>(type: "INTEGER", nullable: false),
            UnityPrice = table.Column<decimal>(type: "TEXT", nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_SaleItems", x => x.Id);
            table.ForeignKey(
                      name: "FK_SaleItems_Products_ProductId",
                      column: x => x.ProductId,
                      principalTable: "Products",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_SaleItems_Sales_SaleId",
                      column: x => x.SaleId,
                      principalTable: "Sales",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_SaleItems_ProductId",
          table: "SaleItems",
          column: "ProductId");

      migrationBuilder.CreateIndex(
          name: "IX_SaleItems_SaleId",
          table: "SaleItems",
          column: "SaleId");

      migrationBuilder.CreateIndex(
          name: "IX_Sales_ClientId",
          table: "Sales",
          column: "ClientId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "SaleItems");

      migrationBuilder.DropTable(
          name: "Users");

      migrationBuilder.DropTable(
          name: "Products");

      migrationBuilder.DropTable(
          name: "Sales");

      migrationBuilder.DropTable(
          name: "Clients");
    }
  }
}
