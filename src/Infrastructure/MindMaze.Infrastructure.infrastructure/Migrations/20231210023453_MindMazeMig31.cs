using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMaze.Infrastructure.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MindMazeMig31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FakeAnswer1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FakeAnswer2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FakeAnswer3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWordKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    EmailVerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordVerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWordResetExpire_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Opponent_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MyPoint = table.Column<int>(type: "int", nullable: false),
                    OpponentPoint = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Games_Users_Opponent_ID",
                        column: x => x.Opponent_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyFriends",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Friend_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_Token_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Friend_Token_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyFriends", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MyFriends_Users_Friend_ID",
                        column: x => x.Friend_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sender_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recevier_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_Sender_ID",
                        column: x => x.Sender_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesQuestions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Game_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesQuestions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GamesQuestions_Games_Game_ID",
                        column: x => x.Game_ID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesQuestions_Questions_Question_ID",
                        column: x => x.Question_ID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_Opponent_ID",
                table: "Games",
                column: "Opponent_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GamesQuestions_Game_ID",
                table: "GamesQuestions",
                column: "Game_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GamesQuestions_Question_ID",
                table: "GamesQuestions",
                column: "Question_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MyFriends_Friend_ID",
                table: "MyFriends",
                column: "Friend_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Sender_ID",
                table: "Notifications",
                column: "Sender_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesQuestions");

            migrationBuilder.DropTable(
                name: "MyFriends");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
