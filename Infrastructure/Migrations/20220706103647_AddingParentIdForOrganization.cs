using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddingParentIdForOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("454d3bfd-3917-4204-84fa-17e2ac126f06"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Organizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Login", "OrganizationId", "Password", "PhoneNumber", "RegionId", "Role" },
                values: new object[] { new Guid("3cedc82f-194f-4f35-8af6-6da877d0111e"), "abdumurodovnodirxon@gmail.com", "Nodirxon", "Abdumurotov", "Nodirkhan", null, "12345", "+998900255013", new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("3cedc82f-194f-4f35-8af6-6da877d0111e"));

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Organizations");

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Login", "OrganizationId", "Password", "PhoneNumber", "RegionId", "Role" },
                values: new object[] { new Guid("454d3bfd-3917-4204-84fa-17e2ac126f06"), "abdumurodovnodirxon@gmail.com", "Nodirxon", "Abdumurotov", "Nodirkhan", null, "12345", "+998900255013", new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"), 0 });
        }
    }
}
