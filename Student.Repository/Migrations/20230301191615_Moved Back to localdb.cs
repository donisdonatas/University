using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniExam.Repository.Migrations
{
    /// <inheritdoc />
    public partial class MovedBacktolocaldb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonalNumber",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalNumber",
                table: "Students");
        }
    }
}
