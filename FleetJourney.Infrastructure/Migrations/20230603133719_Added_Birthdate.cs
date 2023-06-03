using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetJourney.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Birthdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Birthdate",
                table: "Employees",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Employees");
        }
    }
}
