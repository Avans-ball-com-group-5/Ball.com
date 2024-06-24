using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogisticsSQLInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogisticsCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerKm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticsCompanies", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogisticsCompanies");
        }
    }
}
