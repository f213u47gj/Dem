using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskRequestDem.Migrations
{
    /// <inheritdoc />
    public partial class _123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClintId",
                table: "Requests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClintId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
