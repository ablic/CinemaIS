using System.ComponentModel.DataAnnotations;

namespace CinemaIS.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; } = "[NAMELESS]";

        [Display(Name = "Полное название")]
        public string? FullName { get; set; } = "[NAMELESS]";

        public ICollection<Movie> Movies { get; set; }

        public Country()
        {
            Movies = new List<Movie>();
        }
    }
}
