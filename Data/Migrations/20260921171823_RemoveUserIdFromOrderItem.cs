using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoxVendor.WebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserIdFromOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_AspNetUsers_ApplicationUserId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ApplicationUserId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "OrderItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "OrderItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ApplicationUserId",
                table: "OrderItem",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_AspNetUsers_ApplicationUserId",
                table: "OrderItem",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
