using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InStudio.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Agency_Option_In_Profile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAgency",
                table: "UserProfile",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAgency",
                table: "UserProfile");
        }
    }
}
