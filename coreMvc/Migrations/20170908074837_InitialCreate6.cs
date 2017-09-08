using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coreMvc.Migrations
{
    public partial class InitialCreate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagination",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Display = table.Column<bool>(type: "bit", nullable: false),
                    Next = table.Column<int>(type: "int", nullable: false),
                    NextClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Previous = table.Column<int>(type: "int", nullable: false),
                    PreviousClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagination", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PageEntity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Page = table.Column<int>(type: "int", nullable: false),
                    PaginationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageEntity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PageEntity_Pagination_PaginationID",
                        column: x => x.PaginationID,
                        principalTable: "Pagination",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageEntity_PaginationID",
                table: "PageEntity",
                column: "PaginationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageEntity");

            migrationBuilder.DropTable(
                name: "Pagination");
        }
    }
}
