using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Match.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Trump = table.Column<List<string>>(nullable: true),
                    build = table.Column<List<string>>(nullable: true),
                    chipotle = table.Column<bool>(nullable: false),
                    cigarettes = table.Column<List<string>>(nullable: true),
                    diet = table.Column<List<string>>(nullable: true),
                    drinking = table.Column<List<string>>(nullable: true),
                    education = table.Column<List<string>>(nullable: true),
                    ethnicity = table.Column<List<string>>(nullable: true),
                    exercise = table.Column<List<string>>(nullable: true),
                    future_kids = table.Column<List<string>>(nullable: true),
                    height = table.Column<List<int>>(nullable: true),
                    history = table.Column<List<string>>(nullable: true),
                    horoscope = table.Column<List<string>>(nullable: true),
                    interests = table.Column<List<string>>(nullable: true),
                    marijuana = table.Column<List<string>>(nullable: true),
                    max_age = table.Column<int>(nullable: false),
                    memes = table.Column<int>(nullable: false),
                    min_age = table.Column<int>(nullable: false),
                    netflix = table.Column<List<string>>(nullable: true),
                    pets = table.Column<List<string>>(nullable: true),
                    present_kids = table.Column<List<string>>(nullable: true),
                    religion = table.Column<List<string>>(nullable: true),
                    salary = table.Column<List<int>>(nullable: true),
                    sex = table.Column<List<string>>(nullable: true),
                    tattoos = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MatchIds = table.Column<List<int>>(nullable: true),
                    MatchPercentages = table.Column<List<int>>(nullable: true),
                    PreferenceId = table.Column<int>(nullable: false),
                    Trump = table.Column<string>(nullable: true),
                    age = table.Column<int>(nullable: false),
                    build = table.Column<string>(nullable: true),
                    chipotle = table.Column<bool>(nullable: false),
                    cigarettes = table.Column<string>(nullable: true),
                    diet = table.Column<string>(nullable: true),
                    drinking = table.Column<string>(nullable: true),
                    education = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    ethnicity = table.Column<string>(nullable: true),
                    exercise = table.Column<string>(nullable: true),
                    future_kids = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true),
                    height = table.Column<int>(nullable: false),
                    history = table.Column<string>(nullable: true),
                    horoscope = table.Column<string>(nullable: true),
                    interests = table.Column<List<string>>(nullable: true),
                    marijuana = table.Column<string>(nullable: true),
                    memes = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    netflix = table.Column<List<string>>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    pets = table.Column<string>(nullable: true),
                    picture = table.Column<byte>(nullable: false),
                    present_kids = table.Column<string>(nullable: true),
                    religion = table.Column<string>(nullable: true),
                    salary = table.Column<int>(nullable: false),
                    seeking = table.Column<string>(nullable: true),
                    sex = table.Column<string>(nullable: true),
                    tattoos = table.Column<bool>(nullable: false),
                    username = table.Column<string>(nullable: true),
                    zipcode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PersonLikedId = table.Column<int>(nullable: false),
                    PersonLikingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_Likes_Users_PersonLikedId",
                        column: x => x.PersonLikedId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Users_PersonLikingId",
                        column: x => x.PersonLikingId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Content = table.Column<string>(nullable: true),
                    RecieverId = table.Column<int>(nullable: true),
                    RevieverId = table.Column<int>(nullable: false),
                    SenderId = table.Column<int>(nullable: false),
                    SentAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PersonLikedId",
                table: "Likes",
                column: "PersonLikedId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PersonLikingId",
                table: "Likes",
                column: "PersonLikingId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecieverId",
                table: "Messages",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PreferenceId",
                table: "Users",
                column: "PreferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Preferences");
        }
    }
}
