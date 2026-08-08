using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenPharmaTestApp.Migrations
{
    /// <inheritdoc />
    public partial class Added_Customer_And_TaskList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenPharmaCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenPharmaCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenPharmaTaskLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenPharmaTaskLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenPharmaTaskLists_OpenPharmaCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "OpenPharmaCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenPharmaCustomerTaskLists",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskListId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenPharmaCustomerTaskLists", x => new { x.CustomerId, x.TaskListId });
                    table.ForeignKey(
                        name: "FK_OpenPharmaCustomerTaskLists_OpenPharmaCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "OpenPharmaCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpenPharmaCustomerTaskLists_OpenPharmaTaskLists_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "OpenPharmaTaskLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenPharmaCustomerTaskLists_TaskListId",
                table: "OpenPharmaCustomerTaskLists",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenPharmaTaskLists_CustomerId",
                table: "OpenPharmaTaskLists",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenPharmaCustomerTaskLists");

            migrationBuilder.DropTable(
                name: "OpenPharmaTaskLists");

            migrationBuilder.DropTable(
                name: "OpenPharmaCustomers");
        }
    }
}
