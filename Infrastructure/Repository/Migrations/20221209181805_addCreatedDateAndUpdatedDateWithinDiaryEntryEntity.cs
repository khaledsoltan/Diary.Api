using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Diary.Migrations
{
    /// <inheritdoc />
    public partial class addCreatedDateAndUpdatedDateWithinDiaryEntryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "DiaryRoles",
                keyColumn: "Id",
                keyValue: "4c5e8353-9a72-450f-8ff9-7ba4d946548c");

            migrationBuilder.RenameColumn(
                name: "DataChanged",
                table: "DiaryEntry",
                newName: "UpdatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DiaryEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DiaryRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eaadd9f3-c2af-42cc-8563-296a8051ea25", "8026628a-f723-4670-8683-e518cdbe3384", "Basic", "BASIC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "DiaryRoles",
                keyColumn: "Id",
                keyValue: "eaadd9f3-c2af-42cc-8563-296a8051ea25");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DiaryEntry");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "DiaryEntry",
                newName: "DataChanged");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DiaryRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c5e8353-9a72-450f-8ff9-7ba4d946548c", "15e379d1-95d9-4122-aeaf-f4df4972c245", "Basic", "BASIC" });
        }
    }
}
