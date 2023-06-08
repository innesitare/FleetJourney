using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetJourney.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_EmailAsAlternativeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Employees_Email",
                table: "Employees",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Employees_Email",
                table: "Employees");
        }
    }
}
