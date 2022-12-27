namespace Restaurant.Application.ViewModels
{
    public sealed class DayOfWorkViewModel
    {
        public DayOfWeek DayOfWeek { get; set; }
        public int OpensAt { get; set; }
        public int ClosesAt { get; set; }
    }
}
