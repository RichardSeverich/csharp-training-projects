﻿// <auto-generated />
using System;
using Dev31.TodoApp.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dev31.TodoApp.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210210222345_FourthMigration")]
    partial class FourthMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dev31.TodoApp.Models.Project", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("Parent")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uuid");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.Tag", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.TaskTag", b =>
                {
                    b.Property<int>("Id_task")
                        .HasColumnType("int");

                    b.Property<string>("Name_Tag")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id_task", "Name_Tag");

                    b.HasIndex("Name_Tag");

                    b.ToTable("TaskTags");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.TodoTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Depends")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Due")
                        .HasMaxLength(27)
                        .HasColumnType("nvarchar(27)");

                    b.Property<string>("End")
                        .HasMaxLength(27)
                        .HasColumnType("nvarchar(27)");

                    b.Property<string>("Entry")
                        .IsRequired()
                        .HasMaxLength(27)
                        .HasColumnType("nvarchar(27)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProjectUuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Start")
                        .HasMaxLength(27)
                        .HasColumnType("nvarchar(27)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<Guid>("Uuid")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectUuid");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.TaskTag", b =>
                {
                    b.HasOne("Dev31.TodoApp.Models.TodoTask", "TodoTask")
                        .WithMany("TaskTags")
                        .HasForeignKey("Id_task")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dev31.TodoApp.Models.Tag", "Tag")
                        .WithMany("TaskTags")
                        .HasForeignKey("Name_Tag")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("TodoTask");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.TodoTask", b =>
                {
                    b.HasOne("Dev31.TodoApp.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectUuid");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.Tag", b =>
                {
                    b.Navigation("TaskTags");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.TodoTask", b =>
                {
                    b.Navigation("TaskTags");
                });
#pragma warning restore 612, 618
        }
    }
}
