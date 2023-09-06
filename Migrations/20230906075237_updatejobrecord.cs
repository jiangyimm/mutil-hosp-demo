using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace multihospdemo.Migrations.SingletonDb
{
    /// <inheritdoc />
    public partial class updatejobrecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "execute_at",
                schema: "mrqcs",
                table: "job_record",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "execute_at",
                schema: "mrqcs",
                table: "job_record");
        }
    }
}
