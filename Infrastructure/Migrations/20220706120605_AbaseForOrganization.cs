using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AbaseForOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("3cedc82f-194f-4f35-8af6-6da877d0111e"));

            migrationBuilder.AddColumn<Guid>(
                name: "Creaetedby",
                table: "Organizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Organizations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Organizations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedby",
                table: "Organizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Creaetedby",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Managers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Managers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedby",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Creaetedby", "CreatedDate", "Email", "FirstName", "LastModifiedDate", "LastModifiedby", "LastName", "Login", "OrganizationId", "Password", "PhoneNumber", "RegionId", "Role" },
                values: new object[] { new Guid("f14c5b3a-59ae-4bad-a62e-cde394855cec"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "abdumurodovnodirxon@gmail.com", "Nodirxon", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Abdumurotov", "Nodirkhan", null, "12345", "+998900255013", new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("f14c5b3a-59ae-4bad-a62e-cde394855cec"));

            migrationBuilder.DropColumn(
                name: "Creaetedby",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "LastModifiedby",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Creaetedby",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "LastModifiedby",
                table: "Managers");

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Login", "OrganizationId", "Password", "PhoneNumber", "RegionId", "Role" },
                values: new object[] { new Guid("3cedc82f-194f-4f35-8af6-6da877d0111e"), "abdumurodovnodirxon@gmail.com", "Nodirxon", "Abdumurotov", "Nodirkhan", null, "12345", "+998900255013", new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"), 0 });
        }
    }
}
