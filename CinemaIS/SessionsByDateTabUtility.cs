using CinemaIS.Models;
using CinemaIS.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CinemaIS
{
    public class SessionsByDateTabUtility
    {
        public const int TabsCount = 6;

        public static async Task<ICollection<SessionsByDateTab>> GetTabsAsync(DateTime date, IQueryable<Session> sessions)
        {
            List<SessionsByDateTab> tabs = new List<SessionsByDateTab>();

            for (int i = 0; i < TabsCount - 1; i++)
            {
                DateTime nextDate = ((DateTime)date).AddDays(i);

                tabs.Add(new SessionsByDateTab
                {
                    Date = nextDate,
                    Title = GetTabTitle(nextDate),
                    HtmlId = "date-" + i,
                    Sessions = await sessions.Where(s => s.DateTime.Date == nextDate).ToListAsync()
                });
            }

            return tabs;
        }

        public static string GetTabTitle(DateTime date)
        {
            if (date == DateTime.Today)
                return "Сегодня";

            if (date == DateTime.Today.AddDays(1))
                return "Завтра";

            string result = "";

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday: result += "Понедельник"; break;
                case DayOfWeek.Tuesday: result += "Вторник"; break;
                case DayOfWeek.Wednesday: result += "Среда"; break;
                case DayOfWeek.Thursday: result += "Четверг"; break;
                case DayOfWeek.Friday: result += "Пятница"; break;
                case DayOfWeek.Saturday: result += "Суббота"; break;
                case DayOfWeek.Sunday: result += "Воскресенье"; break;
            }

            result += " " + date.ToString("dd.MM");
            return result;
        }
    }
}
