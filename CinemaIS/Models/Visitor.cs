using Microsoft.AspNetCore.Identity;

namespace CinemaIS.Models
{
    public class Visitor : IdentityUser
    {
        public string Name { get; set; } = "NAMELESS";
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
