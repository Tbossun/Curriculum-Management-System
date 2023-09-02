using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class Courseupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Activities_ActivityId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Courses",
                newName: "ActivitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ActivityId",
                table: "Courses",
                newName: "IX_Courses_ActivitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Activities_ActivitiesId",
                table: "Courses",
                column: "ActivitiesId",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Activities_ActivitiesId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "ActivitiesId",
                table: "Courses",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ActivitiesId",
                table: "Courses",
                newName: "IX_Courses_ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Activities_ActivityId",
                table: "Courses",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }
    }
}
