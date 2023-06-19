using CinemaIS.Models;
using System.ComponentModel.DataAnnotations;

namespace CinemaIS.ViewModels
{
    public class Order
    {
        public Session Session { get; set; }

        [Display(Name = "Адрес электронной почты, на которую пришлём билеты")]
        public string BuyerEmail { get; set; } = string.Empty;

        public List<Ticket> SelectedTickets { get; set; }
    }
}
