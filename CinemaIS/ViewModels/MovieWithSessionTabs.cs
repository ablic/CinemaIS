using CinemaIS.Models;

namespace CinemaIS.ViewModels
{
    public class MovieWithSessionTabs
    {
        public Movie Movie { get; set; }
        public ICollection<SessionsByDateTab> SessionsByDateTabs { get; set; }

        public MovieWithSessionTabs()
        {
            Movie = new Movie();
            SessionsByDateTabs = new List<SessionsByDateTab>();
        }
    }
}
