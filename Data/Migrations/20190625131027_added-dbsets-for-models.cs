using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EerCare.Data.Migrations
{
    public partial class addeddbsetsformodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Since = table.Column<DateTime>(nullable: false),
                    Archived = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    MI = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    SSN = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    DOD = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    ProviderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Since = table.Column<DateTime>(nullable: false),
                    Archived = table.Column<DateTime>(nullable: false),
                    ProviderName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.ProviderId);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProviderId = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    SettledDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItem",
                columns: table => new
                {
                    LineItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<int>(nullable: false),
                    ProcedureCode = table.Column<double>(nullable: false),
                    ProcedureDesc = table.Column<string>(nullable: true),
                    AmtBilled = table.Column<double>(nullable: false),
                    AmtSettled = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItem", x => x.LineItemId);
                    table.ForeignKey(
                        name: "FK_LineItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_MemberId",
                table: "Invoice",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ProviderId",
                table: "Invoice",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItem_InvoiceId",
                table: "LineItem",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItem");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Provider");
        }
    }
}
