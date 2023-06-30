using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WF.Membership.Database.Migrations
{
    /// <inheritdoc />
    public partial class addedurls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MarqueeUrl",
                table: "Films",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbUrl",
                table: "Films",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MarqueeUrl", "ThumbUrl" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MarqueeUrl", "ThumbUrl" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MarqueeUrl", "ThumbUrl" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarqueeUrl",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "ThumbUrl",
                table: "Films");
        }
    }
}
