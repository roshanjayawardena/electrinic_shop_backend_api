using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electronic_Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_shippingdetail_to_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShippingDetails_OrderId",
                table: "ShippingDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_OrderId",
                table: "ShippingDetails",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShippingDetails_OrderId",
                table: "ShippingDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_OrderId",
                table: "ShippingDetails",
                column: "OrderId");
        }
    }
}
