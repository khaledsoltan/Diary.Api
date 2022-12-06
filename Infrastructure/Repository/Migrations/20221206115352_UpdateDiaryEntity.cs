using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Diary.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiaryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "DiaryRoles",
                keyColumn: "Id",
                keyValue: "8a6f0020-9d53-4597-86a6-2ab584fe2968");

            migrationBuilder.RenameColumn(
                name: "WorkSpace",
                table: "Diary",
                newName: "DiaryName");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Diary",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Diary",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DiaryRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c5e8353-9a72-450f-8ff9-7ba4d946548c", "15e379d1-95d9-4122-aeaf-f4df4972c245", "Basic", "BASIC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "DiaryRoles",
                keyColumn: "Id",
                keyValue: "4c5e8353-9a72-450f-8ff9-7ba4d946548c");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Diary");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Diary");

            migrationBuilder.RenameColumn(
                name: "DiaryName",
                table: "Diary",
                newName: "WorkSpace");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DiaryRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8a6f0020-9d53-4597-86a6-2ab584fe2968", "821d010f-c02c-44be-8cdd-4b7f93d4201d", "Basic", "BASIC" });
        }
    }
}
