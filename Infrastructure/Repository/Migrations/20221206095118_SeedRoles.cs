using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Diary.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DiaryRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8a6f0020-9d53-4597-86a6-2ab584fe2968", "821d010f-c02c-44be-8cdd-4b7f93d4201d", "Basic", "BASIC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "DiaryRoles",
                keyColumn: "Id",
                keyValue: "8a6f0020-9d53-4597-86a6-2ab584fe2968");
        }
    }
}
