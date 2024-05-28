using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oktmo.Migrations
{
    /// <inheritdoc />
    public partial class Names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kod1Name",
                table: "OktmoEntry",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Kod2Name",
                table: "OktmoEntry",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TerName",
                table: "OktmoEntry",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kod1Name",
                table: "OktmoEntry");

            migrationBuilder.DropColumn(
                name: "Kod2Name",
                table: "OktmoEntry");

            migrationBuilder.DropColumn(
                name: "TerName",
                table: "OktmoEntry");
        }
    }
}
