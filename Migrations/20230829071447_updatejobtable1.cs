using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace multihospdemo.Migrations
{
    /// <inheritdoc />
    public partial class updatejobtable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "job_todo",
                schema: "mrqcs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    visitid = table.Column<string>(name: "visit_id", type: "text", nullable: false),
                    visittype = table.Column<byte>(name: "visit_type", type: "smallint", nullable: false),
                    recordid = table.Column<string>(name: "record_id", type: "text", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "timestamp without time zone", nullable: false),
                    orgcode = table.Column<string>(name: "org_code", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_todo", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job_todo",
                schema: "mrqcs");
        }
    }
}
