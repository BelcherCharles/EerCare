using Microsoft.EntityFrameworkCore.Migrations;

namespace EerCare.Data.Migrations
{
    public partial class tryingtounbustlogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Member_MemberId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Provider_ProviderId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_MemberId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ProviderId",
                table: "Invoice");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
