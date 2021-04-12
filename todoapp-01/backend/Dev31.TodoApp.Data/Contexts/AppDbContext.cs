// <copyright file="AppDbContext.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Data.Contexts
{
    using Dev31.TodoApp.Models;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class AppDbContext
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AppDbContext : DbContext
    {

        /// <summary>
        /// override method OnModelCreating
        /// </summary>
        /// <param name="builder">ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoTask>().ToTable("Tasks");
            //builder.Entity<TodoTask>().HasKey(task => task.Id);
            builder.Entity<TodoTask>().Property(task => task.Uuid).IsRequired().HasMaxLength(36);
           // builder.Entity<TodoTask>().Property(task => task.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<TodoTask>().Property(task => task.Description).IsRequired();
            builder.Entity<TodoTask>().Property(task => task.Priority).IsRequired();
            builder.Entity<TodoTask>().Property(task => task.Status).IsRequired().HasMaxLength(11);
            builder.Entity<TodoTask>().Property(task => task.Start).HasMaxLength(27);
            builder.Entity<TodoTask>().Property(task => task.Due).HasMaxLength(27);
            builder.Entity<TodoTask>().Property(task => task.End).HasMaxLength(27);
            builder.Entity<TodoTask>().Property(task => task.Entry).HasMaxLength(27);
            builder.Entity<TodoTask>().Property(task => task.Depends);


            builder.Entity<TaskTag>().ToTable("TaskTags");
            builder.Entity<TaskTag>().HasKey(taskTag => new { taskTag.Id_task, taskTag.Id_Tag });
            builder.Entity<TaskTag>().HasOne(taskTag => taskTag.Task)
                                     .WithMany(task => task.Tags)
                                     .HasForeignKey(taskTag => taskTag.Id_task);
            builder.Entity<TaskTag>().HasOne(taskTag => taskTag.Tag)
                                     .WithMany(tag => tag.Tasks)
                                     .HasForeignKey(taskTag => taskTag.Id_Tag);

            builder.Entity<Tag>().ToTable("Tags");
            builder.Entity<Tag>().HasKey(tag => tag.Id);
            builder.Entity<Tag>().HasIndex(tag => new { tag.Name }).IsUnique(true);
            builder.Entity<Tag>().Property(tag => tag.Name);
            builder.Entity<Tag>().Property(tag => tag.Name).IsRequired();

            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<Project>().HasKey(project => project.Uuid);
            builder.Entity<Project>().Property(project => project.Uuid).IsRequired().HasMaxLength(36);
            builder.Entity<Project>().Property(project => project.Parent).HasMaxLength(36);

            builder.Entity<Project>().HasMany(project => project.Tasks)
                                     .WithOne(tag => tag.Project);
        }

        /// <summary>
        /// property Tasks DbSet tasks
        /// </summary>
        public DbSet<TodoTask> Tasks { get; set; }

        /// <summary>
        /// property Tags DbSet tags
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// property TaskTags DbSer relation many to many between tasks and tags
        /// </summary>
        public DbSet<TaskTag> TaskTags { get; set; }

        /// <summary>
        /// property Projects Db set
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Constructor for the AppDbContext
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
