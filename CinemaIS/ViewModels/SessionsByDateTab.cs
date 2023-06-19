using CinemaIS.Models;

namespace CinemaIS.ViewModels
{
    public class SessionsByDateTab
    {
        public DateTime Date { get; set; }
        public string Title { get; set; } = "NOTITLE";
        public string HtmlId { get; set; } = "no-id";
        public ICollection<Session> Sessions { get; set; }

        public SessionsByDateTab()
        {
            Sessions = new List<Session>();
        }
    }
}
