using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oktmo.Migrations
{
    /// <inheritdoc />
    public partial class Mappings2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kod1",
                table: "OktmoEntry");

            migrationBuilder.DropColumn(
                name: "Kod2",
                table: "OktmoEntry");

            migrationBuilder.DropColumn(
                name: "Kod3",
                table: "OktmoEntry");

            migrationBuilder.DropColumn(
                name: "Ter",
                table: "OktmoEntry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kod1",
                table: "OktmoEntry",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Kod2",
                table: "OktmoEntry",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Kod3",
                table: "OktmoEntry",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ter",
                table: "OktmoEntry",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }
    }
}
