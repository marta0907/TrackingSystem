using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class initdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 0 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "JobStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 0 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "Name", "Password", "RoleId" },
                values: new object[] { 1, 45, "zheplinska@gmail.com", "Admin", "Qwerty123", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
