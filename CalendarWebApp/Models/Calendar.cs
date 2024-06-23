using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalendarWebApp.Models
{
    public class Calendar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public List<Event> Events { get; set; } = [];
    }
}
