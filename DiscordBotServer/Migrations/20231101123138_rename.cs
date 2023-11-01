using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscordBotServer.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChatLogId",
                table: "Guilds",
                newName: "ChannelLogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChannelLogId",
                table: "Guilds",
                newName: "ChatLogId");
        }
    }
}
