using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learning_asp_core.Migrations
{
    /// <inheritdoc />
    public partial class AddTableWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bss",
                table: "Workflows");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Workflows",
                newName: "WorkItemUrl");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workflows",
                newName: "WorkflowID");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Workflows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Workflows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WorkItemID",
                table: "Workflows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkItemType",
                table: "Workflows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "WorkItemID",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "WorkItemType",
                table: "Workflows");

            migrationBuilder.RenameColumn(
                name: "WorkItemUrl",
                table: "Workflows",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "WorkflowID",
                table: "Workflows",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "Bss",
                table: "Workflows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
