using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYM_MANAGEMENT.Data.Migrations
{
    /// <inheritdoc />
    public partial class SubsTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Subscriptions");
        }
    }
}
