using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartKitchen.Migrations
{
    public partial class UserValidationsAndCalenderImprovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlertMessageValidity",
                table: "ImportantDate",
                newName: "NotificationMessage");

            migrationBuilder.RenameColumn(
                name: "AlertMessage",
                table: "ImportantDate",
                newName: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kitchen",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LocationLongitude",
                table: "Kitchen",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<double>(
                name: "LocationLatitudes",
                table: "Kitchen",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<DateTime>(
                name: "NotificationStartDateTime",
                table: "ImportantDate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Calendar",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationStartDateTime",
                table: "ImportantDate");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Calendar");

            migrationBuilder.RenameColumn(
                name: "NotificationMessage",
                table: "ImportantDate",
                newName: "AlertMessageValidity");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ImportantDate",
                newName: "AlertMessage");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kitchen",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "LocationLongitude",
                table: "Kitchen",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LocationLatitudes",
                table: "Kitchen",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
