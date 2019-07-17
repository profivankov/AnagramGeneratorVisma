using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchedWords",
                columns: table => new
                {
                    SearchedWordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SearchedWord = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedWords", x => x.SearchedWordId);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordID);
                });

            migrationBuilder.CreateTable(
                name: "UserLog",
                columns: table => new
                {
                    LogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserIP = table.Column<string>(nullable: true),
                    SearchedWordID = table.Column<int>(nullable: false),
                    SearchTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLog", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_UserLog_SearchedWords_SearchedWordID",
                        column: x => x.SearchedWordID,
                        principalTable: "SearchedWords",
                        principalColumn: "SearchedWordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CachedWords",
                columns: table => new
                {
                    CacheID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SearchedWordID = table.Column<int>(nullable: false),
                    WordID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedWords", x => x.CacheID);
                    table.ForeignKey(
                        name: "FK_CachedWords_SearchedWords_SearchedWordID",
                        column: x => x.SearchedWordID,
                        principalTable: "SearchedWords",
                        principalColumn: "SearchedWordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CachedWords_Words_WordID",
                        column: x => x.WordID,
                        principalTable: "Words",
                        principalColumn: "WordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CachedWords_SearchedWordID",
                table: "CachedWords",
                column: "SearchedWordID");

            migrationBuilder.CreateIndex(
                name: "IX_CachedWords_WordID",
                table: "CachedWords",
                column: "WordID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLog_SearchedWordID",
                table: "UserLog",
                column: "SearchedWordID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CachedWords");

            migrationBuilder.DropTable(
                name: "UserLog");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "SearchedWords");
        }
    }
}
