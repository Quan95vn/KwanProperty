using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KwanProperty.IdentityServer4.Migrations.IdentityDb
{
    public partial class AddUser_UserClaim_AndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Username = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SecurityCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SecurityCodeExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Provider = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ProviderIdentityKey = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSecrets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Secret = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSecrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSecrets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Email", "Password", "SecurityCode", "SecurityCodeExpirationDate", "Subject", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), true, "7a8be433-fefa-40a5-a34e-be0496c26fde", null, "12345678", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "00000000-0000-0000-0000-000000000001", "Quan" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), true, "00054514-c746-45ac-8dd2-f8ac0bcc6e89", null, "password", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "00000000-0000-0000-0000-000000000002", "Mai" }
                });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("572234ac-eaba-4499-be3c-37505f5cdafd"), "f24786aa-9548-4949-bd76-18a6cee1b9f5", "given_name", new Guid("00000000-0000-0000-0000-000000000001"), "Quan" },
                    { new Guid("a9ad0b31-15ca-4b5e-9ed7-38edaa14bab8"), "4f598f92-1115-4386-b1ce-6456b39bb406", "family_name", new Guid("00000000-0000-0000-0000-000000000001"), "Tran Ngoc Quan" },
                    { new Guid("14bb7184-9e57-42f5-b0d7-c4dbd380b740"), "9a8561fd-dc5a-4042-aaee-fb06288b777d", "email", new Guid("00000000-0000-0000-0000-000000000001"), "tranngocquan95vn@gmail.com" },
                    { new Guid("f0e8d4b9-de15-4a86-9523-7614c4bdac9f"), "ce30917c-184b-4d5a-a14b-fdf0ce6e805f", "address", new Guid("00000000-0000-0000-0000-000000000001"), "Kham Thien" },
                    { new Guid("13806fad-e083-4e71-9565-f2dcf702b012"), "a3b0a429-15ba-44ec-9f6f-c7037b6a47b9", "country", new Guid("00000000-0000-0000-0000-000000000001"), "vn" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Subject",
                table: "Users",
                column: "Subject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSecrets_UserId",
                table: "UserSecrets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserSecrets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
