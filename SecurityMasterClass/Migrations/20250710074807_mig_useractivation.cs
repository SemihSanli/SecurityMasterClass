using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMasterClass.Migrations
{
    /// <inheritdoc />
    public partial class mig_useractivation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivationCode",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "AspNetUsers");
        }
    }
}
