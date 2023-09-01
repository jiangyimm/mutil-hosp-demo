using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace multihospdemo.Migrations
{
    /// <inheritdoc />
    public partial class jobrecord1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mrqcs");

            migrationBuilder.CreateTable(
                name: "job_record",
                schema: "mrqcs",
                columns: table => new
                {
                    queueid = table.Column<string>(name: "queue_id", type: "text", nullable: false),
                    jcommand = table.Column<JsonElement>(name: "j_command", type: "jsonb", nullable: false),
                    executeafter = table.Column<DateTime>(name: "execute_after", type: "timestamp without time zone", nullable: false),
                    expireon = table.Column<DateTime>(name: "expire_on", type: "timestamp without time zone", nullable: false),
                    iscomplete = table.Column<bool>(name: "is_complete", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_record", x => x.queueid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job_record",
                schema: "mrqcs");
        }
    }
}
