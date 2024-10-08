using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InStudio.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_UserProfileId_for_Project_And_UserProfileDesignCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserProfileDesignCategory");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "UserProfileDesignCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileDesignCategory_UserProfileId",
                table: "UserProfileDesignCategory",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserProfileId",
                table: "Project",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_UserProfile1",
                table: "Project",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileDesignCategory_UserProfile",
                table: "UserProfileDesignCategory",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_UserProfile1",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileDesignCategory_UserProfile",
                table: "UserProfileDesignCategory");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileDesignCategory_UserProfileId",
                table: "UserProfileDesignCategory");

            migrationBuilder.DropIndex(
                name: "IX_Project_UserProfileId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "UserProfileDesignCategory");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Project");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserProfileDesignCategory",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
