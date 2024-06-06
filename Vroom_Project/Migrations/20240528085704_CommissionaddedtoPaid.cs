using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vroom_Project.Migrations
{
    public partial class CommissionaddedtoPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommissionId",
                table: "PaidCommissions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaidCommissions_CommissionId",
                table: "PaidCommissions",
                column: "CommissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaidCommissions_Commissions_CommissionId",
                table: "PaidCommissions",
                column: "CommissionId",
                principalTable: "Commissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaidCommissions_Commissions_CommissionId",
                table: "PaidCommissions");

            migrationBuilder.DropIndex(
                name: "IX_PaidCommissions_CommissionId",
                table: "PaidCommissions");

            migrationBuilder.DropColumn(
                name: "CommissionId",
                table: "PaidCommissions");
        }
    }
}
