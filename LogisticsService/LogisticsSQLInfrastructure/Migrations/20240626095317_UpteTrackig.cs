using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogisticsSQLInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpteTrackig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LogisticsCompanies",
                keyColumn: "Id",
                keyValue: new Guid("748f2655-ff04-4dbb-a0ec-2ef9c027e257"));

            migrationBuilder.DeleteData(
                table: "LogisticsCompanies",
                keyColumn: "Id",
                keyValue: new Guid("926e1b92-9755-41ce-a6ac-4c0e87f1c590"));

            migrationBuilder.DeleteData(
                table: "LogisticsCompanies",
                keyColumn: "Id",
                keyValue: new Guid("94e4fb8f-bc46-4dd0-901c-48dcaf84f25c"));

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogisticsCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_LogisticsCompanies_LogisticsCompanyId",
                        column: x => x.LogisticsCompanyId,
                        principalTable: "LogisticsCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemRef",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemRef", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemRef_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trackings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trackings_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LogisticsCompanies",
                columns: new[] { "Id", "Location", "Name", "PricePerKm", "Website" },
                values: new object[,]
                {
                    { new Guid("2e3ae9f4-6739-48aa-92ec-abfe918d59d6"), "Breda", "MailNL", 0.5m, "https://tracking.postnl.nl/track-and-trace/" },
                    { new Guid("b2e5fd60-ce5c-4829-9810-0cdd4ad8ba8e"), "Tilburg", "BHL", 0.4m, "https://www.dhlexpress.nl/nl/consument/track-trace" },
                    { new Guid("c90c8343-1c7d-4b4f-9617-9f295d04feb0"), "Eindhoven", "FredEx", 0.6m, "https://www.fedex.com/nl-nl/tracking.html" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemRef_OrderId",
                table: "ItemRef",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_LogisticsCompanyId",
                table: "Order",
                column: "LogisticsCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Trackings_OrderId",
                table: "Trackings",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemRef");

            migrationBuilder.DropTable(
                name: "Trackings");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DeleteData(
                table: "LogisticsCompanies",
                keyColumn: "Id",
                keyValue: new Guid("2e3ae9f4-6739-48aa-92ec-abfe918d59d6"));

            migrationBuilder.DeleteData(
                table: "LogisticsCompanies",
                keyColumn: "Id",
                keyValue: new Guid("b2e5fd60-ce5c-4829-9810-0cdd4ad8ba8e"));

            migrationBuilder.DeleteData(
                table: "LogisticsCompanies",
                keyColumn: "Id",
                keyValue: new Guid("c90c8343-1c7d-4b4f-9617-9f295d04feb0"));

            migrationBuilder.InsertData(
                table: "LogisticsCompanies",
                columns: new[] { "Id", "Location", "Name", "PricePerKm", "Website" },
                values: new object[,]
                {
                    { new Guid("748f2655-ff04-4dbb-a0ec-2ef9c027e257"), "Eindhoven", "FredEx", 0.6m, "https://www.fedex.com/nl-nl/tracking.html" },
                    { new Guid("926e1b92-9755-41ce-a6ac-4c0e87f1c590"), "Breda", "MailNL", 0.5m, "https://tracking.postnl.nl/track-and-trace/" },
                    { new Guid("94e4fb8f-bc46-4dd0-901c-48dcaf84f25c"), "Tilburg", "BHL", 0.4m, "https://www.dhlexpress.nl/nl/consument/track-trace" }
                });
        }
    }
}
