namespace CalendarWebApp.ViewModels
{
    public class CalendarDetailsViewModel
    {
        public DateOnly Date { get; set; }
        public List<Models.Event> Events { get; set; } = [];
    }
}
