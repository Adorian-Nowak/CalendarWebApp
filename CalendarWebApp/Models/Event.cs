using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CalendarWebApp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Calendar")]
        public int CalendarId { get; set; }
        public Calendar? Calendar { get; set; }

        [Required, MaxLength(20)]
        public string? Name { get; set; }

        [Required, MaxLength(200)]
        public string? Description { get; set; }

        public TimeOnly? Time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    }
}
