using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGP.Infrastructure.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class Initional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "discord",
                columns: table => new
                {
                    discord_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    uuid = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_update = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "online",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    x = table.Column<double>(type: "double", nullable: false),
                    y = table.Column<double>(type: "double", nullable: false),
                    z = table.Column<double>(type: "double", nullable: false),
                    world = table.Column<int>(type: "int(11)", nullable: false),
                    timed_id = table.Column<int>(type: "int(11)", nullable: true),
                    is_op = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    skin_url = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "online_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    day = table.Column<DateOnly>(type: "date", nullable: false),
                    sec = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id, x.day })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    uuid = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    first_name = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    male = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    birthday_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    phone = table.Column<int>(type: "int(11)", nullable: true),
                    card_id = table.Column<int>(type: "int(11)", nullable: true),
                    role = table.Column<int>(type: "int(11)", nullable: false),
                    work = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'0'"),
                    work_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    role_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    connect_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    exp = table.Column<int>(type: "int(11)", nullable: false),
                    last_update = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateIndex(
                name: "discord_id",
                table: "discord",
                column: "discord_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uuid",
                table: "discord",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "phone",
                table: "users",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uuid",
                table: "users",
                column: "uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discord");

            migrationBuilder.DropTable(
                name: "online");

            migrationBuilder.DropTable(
                name: "online_logs");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
