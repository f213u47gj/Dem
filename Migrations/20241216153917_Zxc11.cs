using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskRequestDem.Migrations
{
    /// <inheritdoc />
    public partial class Zxc11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Equipment",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipment",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Requests");
        }
    }
}
