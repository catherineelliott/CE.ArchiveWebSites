using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.ArchiveWebSites.Core.Migrations
{
    public partial class AddOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "ShoppingCartItems");

            migrationBuilder.AlterColumn<int>(
                name: "Finish",
                table: "ShoppingCartItems",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "ShoppingCartItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OrderSizeId",
                table: "ShoppingCartItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "ShoppingCartItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    AddressLine1 = table.Column<string>(maxLength: 100, nullable: false),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(maxLength: 10, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 25, nullable: false),
                    SpecialRequests = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    OrderTotal = table.Column<decimal>(nullable: false),
                    OrderPlaced = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderSizes",
                columns: table => new
                {
                    OrderSizeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSizes", x => x.OrderSizeId);
                });

            migrationBuilder.CreateTable(
                name: "OrderCosts",
                columns: table => new
                {
                    OrderCostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeId = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    PrintFinish = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCosts", x => x.OrderCostId);
                    table.ForeignKey(
                        name: "FK_OrderCosts_OrderSizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "OrderSizes",
                        principalColumn: "OrderSizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    MediaResourceId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    SizeId = table.Column<int>(nullable: false),
                    Sepia = table.Column<bool>(nullable: false),
                    Finish = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderSizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "OrderSizes",
                        principalColumn: "OrderSizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderSizes",
                columns: new[] { "OrderSizeId", "Size" },
                values: new object[] { 1, "10 x 8 inches" });

            migrationBuilder.InsertData(
                table: "OrderSizes",
                columns: new[] { "OrderSizeId", "Size" },
                values: new object[] { 2, "12 x 9 inches" });

            migrationBuilder.InsertData(
                table: "OrderSizes",
                columns: new[] { "OrderSizeId", "Size" },
                values: new object[] { 3, "16 x 12 inches" });

            migrationBuilder.InsertData(
                table: "OrderCosts",
                columns: new[] { "OrderCostId", "Cost", "PrintFinish", "SizeId" },
                values: new object[,]
                {
                    { 1, 6.67m, 0, 1 },
                    { 2, 6.67m, 1, 1 },
                    { 3, 9.17m, 0, 2 },
                    { 4, 9.17m, 1, 2 },
                    { 5, 10.84m, 0, 3 },
                    { 6, 10.84m, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_OrderSizeId",
                table: "ShoppingCartItems",
                column: "OrderSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCosts_SizeId",
                table: "OrderCosts",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_SizeId",
                table: "OrderDetails",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_OrderSizes_OrderSizeId",
                table: "ShoppingCartItems",
                column: "OrderSizeId",
                principalTable: "OrderSizes",
                principalColumn: "OrderSizeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_OrderSizes_OrderSizeId",
                table: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "OrderCosts");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderSizes");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_OrderSizeId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "OrderSizeId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "ShoppingCartItems");

            migrationBuilder.AlterColumn<string>(
                name: "Finish",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
