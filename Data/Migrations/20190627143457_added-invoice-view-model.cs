using Microsoft.EntityFrameworkCore.Migrations;

namespace EerCare.Data.Migrations
{
    public partial class addedinvoiceviewmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Member_MemberId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Provider_ProviderId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_LineItem_Invoice_InvoiceId",
                table: "LineItem");

            migrationBuilder.DropIndex(
                name: "IX_LineItem_InvoiceId",
                table: "LineItem");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_MemberId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ProviderId",
                table: "Invoice");

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProviderType",
            //    table: "Provider",
            //    nullable: true,
            //    oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "ProviderType",
            //    table: "Provider",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineItem_InvoiceId",
                table: "LineItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_MemberId",
                table: "Invoice",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ProviderId",
                table: "Invoice",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Member_MemberId",
                table: "Invoice",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Provider_ProviderId",
                table: "Invoice",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "ProviderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LineItem_Invoice_InvoiceId",
                table: "LineItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
