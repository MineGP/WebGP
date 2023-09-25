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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
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
                    data_icon = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_name = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_op = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    zone_selector = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    skin_url = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    die = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    gpose = table.Column<string>(type: "enum('SIT','LAY','NONE')", nullable: false, defaultValueSql: "'NONE'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hide = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    last_update = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
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
                    sec = table.Column<int>(type: "int(11)", nullable: false),
                    sec_aban = table.Column<int>(type: "int(11)", nullable: false),
                    sec_afk = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id, x.day })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    color = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false, defaultValueSql: "'FFFFFF'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    permissions = table.Column<int>(type: "int(11)", nullable: false),
                    perm_menu = table.Column<long>(type: "bigint(20)", nullable: false),
                    perm_menu_local = table.Column<long>(type: "bigint(20)", nullable: false),
                    id_group = table.Column<int>(type: "int(11)", nullable: false),
                    @static = table.Column<sbyte>(name: "static", type: "tinyint(4)", nullable: false),
                    discord_role = table.Column<long>(type: "bigint(20)", nullable: true),
                    head_data = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_update = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
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
                    phone_regen = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'3'"),
                    card_regen = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'1'"),
                    wanted = table.Column<int>(type: "int(11)", nullable: false),
                    wanted_id = table.Column<int>(type: "int(11)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    connect_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    exp = table.Column<int>(type: "int(11)", nullable: false),
                    last_update = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "work_readonly",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    type = table.Column<string>(type: "enum('WORK')", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id, x.type })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
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
            // creating view
            migrationBuilder.Sql("CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `role_work_readonly` AS select `work_readonly`.`id` AS `id`,`work_readonly`.`type` AS `type`,`work_readonly`.`icon` AS `icon`,`work_readonly`.`name` AS `name` from `work_readonly` union select `roles`.`id` AS `id`,'ROLE' AS `type`,if(octet_length(`roles`.`name`) = 0,'',concat('<#',`roles`.`color`,'>',`roles`.`name`)) AS `icon`,`roles`.`name` AS `name` from `roles`;");
            migrationBuilder.Sql(
                "CREATE FUNCTION `GetLevel`(`_exp` INT) RETURNS int(11)BEGIN    DECLARE _level INT DEFAULT 0;    DECLARE _next INT DEFAULT 0;    WHILE (TRUE) DO        SET _next = _next + _level * 4;        IF (_exp < _next) THEN            RETURN _level - 1;        END IF;        SET _level = _level + 1;    END WHILE;END");
            migrationBuilder.Sql("CREATE FUNCTION `GetExp`(`_level` INT) RETURNS int(11)BEGIN    DECLARE _next INT DEFAULT 0;    WHILE (TRUE) DO        SET _next = _next + _level * 4;        IF (_level = 0) THEN            RETURN _next;        END IF;        SET _level = _level - 1;    END WHILE;END");
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
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "work_readonly");
            migrationBuilder.Sql("DROP VIEW `role_work_readonly`;");
        }
    }
}
