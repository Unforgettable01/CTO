using Microsoft.EntityFrameworkCore.Migrations;

namespace CTODatabaseImplement.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestCosts_Requests_ConferenceId",
                table: "RequestCosts");

            migrationBuilder.DropIndex(
                name: "IX_RequestCosts_ConferenceId",
                table: "RequestCosts");

            migrationBuilder.DropColumn(
                name: "ConferenceId",
                table: "RequestCosts");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCosts_RequestId",
                table: "RequestCosts",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestCosts_Requests_RequestId",
                table: "RequestCosts",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestCosts_Requests_RequestId",
                table: "RequestCosts");

            migrationBuilder.DropIndex(
                name: "IX_RequestCosts_RequestId",
                table: "RequestCosts");

            migrationBuilder.AddColumn<int>(
                name: "ConferenceId",
                table: "RequestCosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestCosts_ConferenceId",
                table: "RequestCosts",
                column: "ConferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestCosts_Requests_ConferenceId",
                table: "RequestCosts",
                column: "ConferenceId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
