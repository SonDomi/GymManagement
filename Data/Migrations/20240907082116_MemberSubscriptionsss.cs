using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYM_MANAGEMENT.Data.Migrations
{
    /// <inheritdoc />
    public partial class MemberSubscriptionsss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptions_MemberId",
                table: "MemberSubscriptions",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscriptions_SubscriptionId",
                table: "MemberSubscriptions",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_Members_MemberId",
                table: "MemberSubscriptions",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_Subscriptions_SubscriptionId",
                table: "MemberSubscriptions",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_Members_MemberId",
                table: "MemberSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_Subscriptions_SubscriptionId",
                table: "MemberSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_MemberSubscriptions_MemberId",
                table: "MemberSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_MemberSubscriptions_SubscriptionId",
                table: "MemberSubscriptions");
        }
    }
}
