namespace CalendarWebApp.ViewModels
{
    public class CalendarDeleteViewModel
    {
        public DateOnly Date { get; set; }
        public List<Models.Event> Events { get; set; } = [];
    }
}
