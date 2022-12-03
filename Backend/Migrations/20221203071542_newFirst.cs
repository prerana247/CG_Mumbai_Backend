using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class newFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PersonalMail = table.Column<string>(nullable: false),
                    CorpMail = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    MobileNumber = table.Column<string>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    DOJ = table.Column<DateTime>(nullable: false),
                    Grade = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Role = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Password = table.Column<string>(maxLength: 8, nullable: false),
                    OTP = table.Column<int>(nullable: false),
                    isVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
