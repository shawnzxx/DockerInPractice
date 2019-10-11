using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Audit.Application.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "runningtotalseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "runningtotals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    RunningTotal = table.Column<double>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_runningtotals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "runningtotals");

            migrationBuilder.DropSequence(
                name: "runningtotalseq");
        }
    }
}
