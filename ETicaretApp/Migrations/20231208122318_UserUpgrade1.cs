using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretUygulamasi.Migrations
{
    /// <inheritdoc />
    public partial class UserUpgrade1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Unique",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unique",
                table: "Users");
        }
    }
}
