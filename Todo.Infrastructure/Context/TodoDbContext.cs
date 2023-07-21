using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Context
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        { }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoItem>().ToTable("Todo");
            builder.Entity<TodoItem>().Property(x => x.Id).IsRequired();
            builder.Entity<TodoItem>().Property(x => x.Title).IsRequired().HasMaxLength(100).HasColumnType("NVARCHAR").HasColumnName("Title");
            builder.Entity<TodoItem>().Property(x => x.User).IsRequired().HasMaxLength(160).HasColumnType("NVARCHAR").HasColumnName("User");
            builder.Entity<TodoItem>().Property(x => x.Done).IsRequired().HasColumnName("Done");
            builder.Entity<TodoItem>().Property(x => x.Date).IsRequired().HasColumnType("DATETIME").HasColumnName("Date");
        }
    }
}