using CalendarWebApp.Models;

namespace CalendarWebApp.ViewModels
{
    public class CalendarCreateViewModel
    {
        public Calendar Calendar { get; set; } = new Calendar();
        public Event Event { get; set; } = new Event();
        public bool AllDay = false;
    }
}
