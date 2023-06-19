using System.ComponentModel.DataAnnotations;

namespace CinemaIS.ViewModels
{
    public class BuyerEmail
    {
        [Required(ErrorMessage = "Укажите электронную почту для отправки билетов")]
        [Display(Name = "Электронная почта, на которую пришлем билеты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
