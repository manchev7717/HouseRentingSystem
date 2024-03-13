using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class UniqueConstaintPhoneNumberAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "172bdedf-9ac5-41b0-94c4-150cd70aaefd", "AQAAAAEAACcQAAAAEJ6T3xNVZ2T3gt3EVwXo0UGI+pGidCvEmpI6Jyd4l3BJ9m1XYJ2GzYbNm9wgFjfQxg==", "3a5c270d-66f0-4c97-86b3-203cc8321459" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f07b613-987b-491e-8ad6-25950521aa82", "AQAAAAEAACcQAAAAEMT/uOtsMAdUSwvIhJ2/5xKI/vqceUMj+4sHRiIcH6jnhdvUebrHVcfWUr5EHA7hwA==", "8fc1bf4b-af0a-4226-b833-987fa0528491" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a49311d6-60ed-456c-90cb-407d4b6e6b27", "AQAAAAEAACcQAAAAELAAbyERxnEd3kdDrwhAsgOFxuncWPx6dJR3OIrhsfhuxUhN2P47257jmQ180NbTSg==", "c563fef4-644c-4a4b-a339-8eeedf5c9b60" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e596e5e-58be-4d12-b2d3-8d1e1df159ae", "AQAAAAEAACcQAAAAEAj2YeOhr3Azm9YAfaJ5pjzHD67OsJvBXrvuyLnarfMHy1fD93s4BctUhIjqeY7PgQ==", "be86b77b-9182-4271-972d-783be45b3d59" });
        }
    }
}
