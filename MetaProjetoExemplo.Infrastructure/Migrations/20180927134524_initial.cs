using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MetaProjetoExemplo.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.EnsureSchema(
                name: "project_management");

            migrationBuilder.CreateTable(
                name: "actions_logs_types",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actions_logs_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    identifier = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project_managers",
                schema: "project_management",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user_identifier = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_managers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "actions_logs",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(nullable: false),
                    user_identifier = table.Column<Guid>(nullable: true),
                    ip_address = table.Column<string>(nullable: true),
                    action_log_type_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actions_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_actions_logs_actions_logs_types_action_log_type_id",
                        column: x => x.action_log_type_id,
                        principalSchema: "common",
                        principalTable: "actions_logs_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                schema: "project_management",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    project_manager_id = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: false),
                    finish_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    manager_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_projects_project_managers_project_manager_id",
                        column: x => x.project_manager_id,
                        principalSchema: "project_management",
                        principalTable: "project_managers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "common",
                table: "actions_logs_types",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "USER_CREATED" },
                    { 2, "USER_LOGIN_ATTEMPT" },
                    { 3, "USER_LOGIN_FAIL" },
                    { 4, "USER_LOGIN_SUCCESS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_actions_logs_action_log_type_id",
                schema: "common",
                table: "actions_logs",
                column: "action_log_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_actions_logs_id",
                schema: "common",
                table: "actions_logs",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_actions_logs_types_id",
                schema: "common",
                table: "actions_logs_types",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                schema: "common",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_id",
                schema: "common",
                table: "users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_users_identifier",
                schema: "common",
                table: "users",
                column: "identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_managers_id",
                schema: "project_management",
                table: "project_managers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_projects_project_manager_id",
                schema: "project_management",
                table: "projects",
                column: "project_manager_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actions_logs",
                schema: "common");

            migrationBuilder.DropTable(
                name: "users",
                schema: "common");

            migrationBuilder.DropTable(
                name: "projects",
                schema: "project_management");

            migrationBuilder.DropTable(
                name: "actions_logs_types",
                schema: "common");

            migrationBuilder.DropTable(
                name: "project_managers",
                schema: "project_management");
        }
    }
}
