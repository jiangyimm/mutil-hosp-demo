﻿// <auto-generated />
using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using multi_hosp_demo.JobQueues;

#nullable disable

namespace multihospdemo.Migrations.SingletonDb
{
    [DbContext(typeof(JobDbContext))]
    [Migration("20230906075237_updatejobrecord")]
    partial class updatejobrecord
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("multi_hosp_demo.JobQueues.JobRecord", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("ExecuteAfter")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("execute_after");

                    b.Property<DateTime?>("ExecuteAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("execute_at");

                    b.Property<DateTime>("ExpireOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expire_on");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("boolean")
                        .HasColumnName("is_complete");

                    b.Property<JsonElement>("JCommand")
                        .HasColumnType("jsonb")
                        .HasColumnName("j_command");

                    b.Property<string>("QueueID")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("queue_id");

                    b.HasKey("ID");

                    b.ToTable("job_record", "mrqcs");
                });
#pragma warning restore 612, 618
        }
    }
}
