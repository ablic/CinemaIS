namespace CinemaIS.ViewModels
{
    public class Seat
    {
        public float LeftGap { get; set; } = 0f;
        public float RightGap { get; set; } = 0f;
        public decimal Price { get; set; }
        public bool IsOccupied { get; set; } = false;
        public bool IsSelected { get; set; } = false;
    }
}
