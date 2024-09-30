using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYM_MANAGEMENT.Data.Migrations
{
    /// <inheritdoc />
    public partial class MemberCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegistrationCard",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullMemberName",
                table: "GridMemberSubscriptionDto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionCode",
                table: "GridMemberSubscriptionDto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MemberFullName",
                table: "EditMemberSubscriptionDto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionDescription",
                table: "EditMemberSubscriptionDto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationCard",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "FullMemberName",
                table: "GridMemberSubscriptionDto");

            migrationBuilder.DropColumn(
                name: "SubscriptionCode",
                table: "GridMemberSubscriptionDto");

            migrationBuilder.DropColumn(
                name: "MemberFullName",
                table: "EditMemberSubscriptionDto");

            migrationBuilder.DropColumn(
                name: "SubscriptionDescription",
                table: "EditMemberSubscriptionDto");
        }
    }
}
