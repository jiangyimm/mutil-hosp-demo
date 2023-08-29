﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using multi_hosp_demo.Entities;

#nullable disable

namespace multihospdemo.Migrations
{
    [DbContext(typeof(QcContext))]
    [Migration("20230829071447_updatejobtable1")]
    partial class updatejobtable1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("multi_hosp_demo.Entities.ExtDept", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DeptCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeptName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HospCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ext_dept", "mrqcs");
                });

            modelBuilder.Entity("multi_hosp_demo.Entities.JobTodo", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_time");

                    b.Property<string>("OrgCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("org_code");

                    b.Property<string>("RecordId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("record_id");

                    b.Property<string>("VisitId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("visit_id");

                    b.Property<byte>("VisitType")
                        .HasColumnType("smallint")
                        .HasColumnName("visit_type");

                    b.HasKey("ID");

                    b.ToTable("job_todo", "mrqcs");
                });
#pragma warning restore 612, 618
        }
    }
}
