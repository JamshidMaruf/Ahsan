using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ahsan.Data.Migrations
{
    public partial class fluentApiRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Companyies_CompanyId",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Positions_PositionId",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Users_EmployeeId",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Companyies_Users_OwnerId",
                table: "Companyies");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueCategories_Companyies_CompanyId",
                table: "IssueCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_CompanyEmployees_AssignedUserId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Companyies_CompanyId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_IssueCategories_CategoryId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssignedUserId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Issues");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedId",
                table: "Issues",
                column: "AssignedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Companyies_CompanyId",
                table: "CompanyEmployees",
                column: "CompanyId",
                principalTable: "Companyies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Positions_PositionId",
                table: "CompanyEmployees",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Users_EmployeeId",
                table: "CompanyEmployees",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companyies_Users_OwnerId",
                table: "Companyies",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueCategories_Companyies_CompanyId",
                table: "IssueCategories",
                column: "CompanyId",
                principalTable: "Companyies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_CompanyEmployees_AssignedId",
                table: "Issues",
                column: "AssignedId",
                principalTable: "CompanyEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Companyies_CompanyId",
                table: "Issues",
                column: "CompanyId",
                principalTable: "Companyies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_IssueCategories_CategoryId",
                table: "Issues",
                column: "CategoryId",
                principalTable: "IssueCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Companyies_CompanyId",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Positions_PositionId",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Users_EmployeeId",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Companyies_Users_OwnerId",
                table: "Companyies");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueCategories_Companyies_CompanyId",
                table: "IssueCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_CompanyEmployees_AssignedId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Companyies_CompanyId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_IssueCategories_CategoryId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssignedId",
                table: "Issues");

            migrationBuilder.AddColumn<long>(
                name: "AssignedUserId",
                table: "Issues",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedUserId",
                table: "Issues",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Companyies_CompanyId",
                table: "CompanyEmployees",
                column: "CompanyId",
                principalTable: "Companyies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Positions_PositionId",
                table: "CompanyEmployees",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Users_EmployeeId",
                table: "CompanyEmployees",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companyies_Users_OwnerId",
                table: "Companyies",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueCategories_Companyies_CompanyId",
                table: "IssueCategories",
                column: "CompanyId",
                principalTable: "Companyies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_CompanyEmployees_AssignedUserId",
                table: "Issues",
                column: "AssignedUserId",
                principalTable: "CompanyEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Companyies_CompanyId",
                table: "Issues",
                column: "CompanyId",
                principalTable: "Companyies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_IssueCategories_CategoryId",
                table: "Issues",
                column: "CategoryId",
                principalTable: "IssueCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
