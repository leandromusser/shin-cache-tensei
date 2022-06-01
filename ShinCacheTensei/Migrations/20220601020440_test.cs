using Microsoft.EntityFrameworkCore.Migrations;

namespace ShinCacheTensei.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecruitingMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitingMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Demons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    RecruitingMethodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Demons_RecruitingMethod_RecruitingMethodId",
                        column: x => x.RecruitingMethodId,
                        principalTable: "RecruitingMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DemonSkill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemonSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemonSkill_Demons_DemonId",
                        column: x => x.DemonId,
                        principalTable: "Demons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Demons_RecruitingMethodId",
                table: "Demons",
                column: "RecruitingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_DemonSkill_DemonId",
                table: "DemonSkill",
                column: "DemonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemonSkill");

            migrationBuilder.DropTable(
                name: "Demons");

            migrationBuilder.DropTable(
                name: "RecruitingMethod");
        }
    }
}
