using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class AddedUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserIP",
                table: "UserLog",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    UserIP = table.Column<string>(nullable: false),
                    SearchesLeft = table.Column<int>(nullable: false),
                    TotalSearches = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.UserIP);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLog_UserIP",
                table: "UserLog",
                column: "UserIP");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLog_UserInfo_UserIP",
                table: "UserLog",
                column: "UserIP",
                principalTable: "UserInfo",
                principalColumn: "UserIP",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLog_UserInfo_UserIP",
                table: "UserLog");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserLog_UserIP",
                table: "UserLog");

            migrationBuilder.AlterColumn<string>(
                name: "UserIP",
                table: "UserLog",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
