using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaIS.Models
{
    public class CinemaDbContext : IdentityDbContext<Visitor>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Visitor> Visitors { get; set; }

        public CinemaDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
