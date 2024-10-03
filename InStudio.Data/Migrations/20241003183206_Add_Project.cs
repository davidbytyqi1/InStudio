using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InStudio.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Project : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignCategoryId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ConnectionsNumber = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmployeeFeedback = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    DesignerFeedback = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Project_DesignCategory",
                        column: x => x.DesignCategoryId,
                        principalTable: "DesignCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_DesignCategoryId",
                table: "Project",
                column: "DesignCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
