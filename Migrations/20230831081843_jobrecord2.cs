using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace multihospdemo.Migrations.SingletonDb
{
    /// <inheritdoc />
    public partial class jobrecord2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_job_record",
                schema: "mrqcs",
                table: "job_record");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "mrqcs",
                table: "job_record",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_job_record",
                schema: "mrqcs",
                table: "job_record",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_job_record",
                schema: "mrqcs",
                table: "job_record");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "mrqcs",
                table: "job_record");

            migrationBuilder.AddPrimaryKey(
                name: "PK_job_record",
                schema: "mrqcs",
                table: "job_record",
                column: "queue_id");
        }
    }
}
