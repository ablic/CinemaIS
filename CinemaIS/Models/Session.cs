using System.ComponentModel.DataAnnotations;

namespace CinemaIS.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        [Display(Name = "Фильм")]
        public Movie? Movie { get; set; }

        [Required(ErrorMessage = "Укажите дату проведения сеанса")]
        [Display(Name = "Дата и время")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Номер зала")]
        public int HallId { get; set; }
        public Hall? Hall { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public Session()
        {
            Tickets = new List<Ticket>();
        }
    }
}
