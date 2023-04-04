using System.ComponentModel.DataAnnotations;

namespace CinemaIS.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public Session? Session { get; set; }

        [Display(Name = "Ряд")]
        public int Row { get; set; }

        [Display(Name = "Место")]
        public int Seat { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}