using System.ComponentModel.DataAnnotations;

namespace CinemaIS.Models
{
    public class Ticket
    {
        public int Id { get; set; }


        [Display(Name = "Сеанс")]
        public int SessionId { get; set; }
        public Session? Session { get; set; }


        [Display(Name = "Место")]
        public string Place { get; set; }


        [Display(Name = "Цена")]
        public decimal Price { get; set; }


        public string? VisitorId { get; set; }
        public Visitor? Visitor { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Электронная почта для отправки билетов")]
        public string? OwnerEmail { get; set; }
    }
}