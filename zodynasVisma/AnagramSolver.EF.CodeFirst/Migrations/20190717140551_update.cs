using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words_WordID",
                table: "CachedWords");

            migrationBuilder.AlterColumn<int>(
                name: "WordID",
                table: "CachedWords",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words_WordID",
                table: "CachedWords",
                column: "WordID",
                principalTable: "Words",
                principalColumn: "WordID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words_WordID",
                table: "CachedWords");

            migrationBuilder.AlterColumn<int>(
                name: "WordID",
                table: "CachedWords",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words_WordID",
                table: "CachedWords",
                column: "WordID",
                principalTable: "Words",
                principalColumn: "WordID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
