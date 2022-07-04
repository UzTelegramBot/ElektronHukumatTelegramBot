using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ForMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("f7a96866-c568-4b0c-be00-4f62b4f1ab2b"));

            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Login", "OrganizationId", "Password", "PhoneNumber", "RegionId", "Role" },
                values: new object[] { new Guid("68008369-419d-4d53-988e-06ebf9454de3"), "abdumurodovnodirxon@gmail.com", "Nodirxon", "Abdumurotov", "Nodirkhan", null, "12345", "+998900255013", new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("68008369-419d-4d53-988e-06ebf9454de3"));

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Login", "OrganizationId", "Password", "PhoneNumber", "RegionId", "Role" },
                values: new object[] { new Guid("f7a96866-c568-4b0c-be00-4f62b4f1ab2b"), "abdumurodovnodirxon@gmail.com", "Nodirxon", "Abdumurotov", "Nodirkhan", null, "12345", "+998900255013", new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"), 0 });
        }
    }
}
