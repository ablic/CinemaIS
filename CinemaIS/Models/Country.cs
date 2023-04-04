namespace CinemaIS.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = "[NAMELESS]";
        public ICollection<Movie> Movies { get; set; }

        public Country()
        {
            Movies = new List<Movie>();
        }
    }
}
