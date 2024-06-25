using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSQLInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ItemRef",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ItemRef");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
