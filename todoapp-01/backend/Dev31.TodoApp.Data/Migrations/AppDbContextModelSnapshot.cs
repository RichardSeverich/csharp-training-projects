﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Dev31.TodoApp.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dev31.TodoApp.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.TaskTag", b =>
                {
                    b.Property<int>("Id_task")
                        .HasColumnType("int");

                    b.Property<int>("Id_Tag")
                        .HasColumnType("int");

                    b.HasKey("Id_task", "Id_Tag");

                    b.HasIndex("Id_Tag");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Due")
                        .HasMaxLength(27)
                        .HasColumnType("nvarchar(27)");

                    b.Property<string>("End")
                        .HasMaxLength(27)
                        .HasColumnType("nvarchar(27)");

                    b.Property<string>("Entry")
                        .HasMaxLength(27)
                        .HasColumnType("nvarchar(27)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProjectUuid")
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

            modelBuilder.Entity("Dev31.TodoApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasMaxLength(27)
                        .HasColumnType("nvarchar(27)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.TaskTag", b =>
                {
                    b.HasOne("Dev31.TodoApp.Models.Tag", "Tag")
                        .WithMany("Tasks")
                        .HasForeignKey("Id_Tag")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dev31.TodoApp.Models.TodoTask", "Task")
                        .WithMany("Tags")
                        .HasForeignKey("Id_task")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.TodoTask", b =>
                {
                    b.HasOne("Dev31.TodoApp.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectUuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.Tag", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Dev31.TodoApp.Models.TodoTask", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
