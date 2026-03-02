using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Haceb.Demanda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DemandTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRols_Role",
                        column: x => x.Role,
                        principalTable: "UserRols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Demands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaintiffName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Prioritize = table.Column<int>(type: "int", nullable: false),
                    RatingId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateRegistry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Demands_Classifications_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Classifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demands_DemandTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DemandTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demands_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DemandHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemandId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateRegistry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandHistories_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Classifications",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Tecnológica" },
                    { 2, "Legal" },
                    { 3, "Operativa" },
                    { 4, "Comercial" }
                });

            migrationBuilder.InsertData(
                table: "DemandTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Proyectos" },
                    { 2, "Requerimientos" },
                    { 3, "Soportes" },
                    { 4, "Incidentes" },
                    { 5, "Temas legales" },
                    { 6, "Vulnerabilidades" },
                    { 7, "Apoyos" }
                });

            migrationBuilder.InsertData(
                table: "UserRols",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "ADM", "Admin" },
                    { 2, "USR", "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "tecnologia@mail.com", true, "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", 1, "Tecnología" },
                    { 2, "Financiera@mail.com", true, "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", 2, "Financiera" },
                    { 3, "Negociacion@mail.com", true, "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", 2, "Negociación" },
                    { 4, "Operaciones@mail.com", true, "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", 2, "Operaciones" },
                    { 5, "IDI@mail.com", true, "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", 2, "I + D + I" },
                    { 6, "Comercial@mail.com", true, "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", 2, "Comercial" },
                    { 7, "Mercadeo@mail.com", true, "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", 2, "Mercadeo" },
                    { 8, "Talentohumano@mail.com", true, "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", 2, "Talento Humano" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemandHistories_DemandId",
                table: "DemandHistories",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandHistories_UserId",
                table: "DemandHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_RatingId",
                table: "Demands",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_TypeId",
                table: "Demands",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_UserId",
                table: "Demands",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandHistories");

            migrationBuilder.DropTable(
                name: "Demands");

            migrationBuilder.DropTable(
                name: "Classifications");

            migrationBuilder.DropTable(
                name: "DemandTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRols");
        }
    }
}
