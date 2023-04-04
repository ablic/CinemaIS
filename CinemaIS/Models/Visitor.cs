using Microsoft.AspNetCore.Identity;

namespace CinemaIS.Models
{
    public class Visitor : IdentityUser
    {
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
