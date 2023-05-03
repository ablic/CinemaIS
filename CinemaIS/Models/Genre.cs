using System.ComponentModel.DataAnnotations;

namespace CinemaIS.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; } = "[NAMELESS]";

        public ICollection<Movie> Movies { get; set; }

        public Genre()
        {
            Movies = new List<Movie>();
        }
    }
}
