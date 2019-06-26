using Microsoft.EntityFrameworkCore.Migrations;

namespace EerCare.Data.Migrations
{
    public partial class addedspecialtyfieldtoprovider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProviderName",
                table: "Provider",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderType",
                table: "Provider",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderType",
                table: "Provider");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderName",
                table: "Provider",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
