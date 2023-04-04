using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaIS.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        [Display(Name = "Фильм")]
        public Movie? Movie { get; set; }

        [Display(Name = "Дата и время")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Зал")]
        public int HallNumber { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public Session()
        {
            Tickets = new List<Ticket>();
        }
    }
}
