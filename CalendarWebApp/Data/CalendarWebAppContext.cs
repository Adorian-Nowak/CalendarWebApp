using CalendarWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarWebApp.Data
{
    public class CalendarWebAppContext(DbContextOptions<CalendarWebAppContext> options) : DbContext(options)
    {
        public DbSet<Calendar> Calendar { get; set; } = default!;
        public DbSet<Event>    Events   { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Calendar>()
                .HasIndex(c => c.Date)
                .IsUnique();

            _ = modelBuilder.Entity<Calendar>().HasData(
                new Calendar { Id = 1, Date = new DateOnly(2024, 5, 17) },
                new Calendar { Id = 2, Date = new DateOnly(2024, 5, 18) }
            );

            _ = modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, CalendarId = 1, Name = "Event 1", Description = "Description 1", Time = new TimeOnly(10, 0) },
                new Event { Id = 2, CalendarId = 1, Name = "Event 2", Description = "Description 2" },
                new Event { Id = 3, CalendarId = 2, Name = "Event 3", Description = "Description 3", Time = new TimeOnly(14, 0) }
            );
        }
    }
}
