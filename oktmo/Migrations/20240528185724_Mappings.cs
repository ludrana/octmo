using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oktmo.Migrations
{
    /// <inheritdoc />
    public partial class Mappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Octmo",
                table: "OktmoEntry",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Octmo",
                table: "OktmoEntry");
        }
    }
}
