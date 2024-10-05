using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationOrder.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderMasters_OrderedIDMaster",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "OrderedIDMaster",
                table: "OrderDetails",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "Item",
                table: "OrderDetails",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderedIDMaster",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ItemId",
                table: "OrderDetails",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Items_ItemId",
                table: "OrderDetails",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderMasters_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "OrderMasters",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Items_ItemId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderMasters_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ItemId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderDetails",
                newName: "OrderedIDMaster");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "OrderDetails",
                newName: "Item");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderedIDMaster");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderMasters_OrderedIDMaster",
                table: "OrderDetails",
                column: "OrderedIDMaster",
                principalTable: "OrderMasters",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
