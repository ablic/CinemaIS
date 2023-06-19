using CinemaIS.Models;
using CinemaIS.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaIS.TagHelpers
{
    public static class SessionsByDateTabHelper
    {
        private const string htmlIdTemplate = "dd-MM";

        /*public static IHtmlContent GetTabsWithSessions(ICollection<SessionsByDateTab> tabs)
        {
            HtmlContentBuilder builder = new HtmlContentBuilder();

            foreach (var tab in tabs)
            {
                builder.AppendHtml(GetTab(
                    tab.Date, 
                    tab.Date.ToString(htmlIdTemplate), 
                    tab.Sessions.Count == 0));
            }

            TagBuilder tabContentBuilder = new TagBuilder("div");
            tabContentBuilder.MergeAttribute("id", "myTabContent");
            tabContentBuilder.MergeAttribute("class", "tab-content");

            foreach (var tab in tabs)
            {
                
            }
        }

        public static IHtmlContent? GetTab(DateTime date, string htmlId, bool isDisabled)
        {
            string tabTitle = GetTabTitle(date);

            TagBuilder li_Builder = new TagBuilder("li");
            li_Builder.MergeAttribute("class", "nav-item");
            li_Builder.MergeAttribute("role", "presentation");

            TagBuilder button_Builder = new TagBuilder("button");
            button_Builder.MergeAttribute("class", "nav-link");

            if (isDisabled)
                button_Builder.MergeAttribute("class", "disabled");

            button_Builder.MergeAttribute("id", htmlId + "-tab");
            button_Builder.MergeAttribute("data-bs-toggle", "tab");
            button_Builder.MergeAttribute("data-bs-target", "#" + htmlId);
            button_Builder.MergeAttribute("type", "button");
            button_Builder.MergeAttribute("role", "tab");
            button_Builder.MergeAttribute("aria-controls", htmlId);
            button_Builder.MergeAttribute("aria-selected", "false");

            button_Builder.InnerHtml.Append(tabTitle);
            li_Builder.InnerHtml.AppendHtml(button_Builder);

            return li_Builder.RenderBody();
        }

        public static IHtmlContent? GetTabContent(ICollection<Session> sessions, string htmlId)
        {
            TagBuilder tabPainBuilder = new TagBuilder("div");
            tabPainBuilder.MergeAttribute("id", htmlId);
            tabPainBuilder.MergeAttribute("class", "tab-pane");
            tabPainBuilder.MergeAttribute("class", "fade");
            tabPainBuilder.MergeAttribute("role", "tabpanel");
            tabPainBuilder.MergeAttribute("aria-labelledby", htmlId + "-tab");

            foreach (var session in sessions)
            {
                TagBuilder a_Builder = new TagBuilder("a");
                a_Builder.MergeAttribute("class", "list-group-item");
                a_Builder.MergeAttribute("class", "list-group-item-action");
                a_Builder.MergeAttribute("asp-action", "Details");
                a_Builder.MergeAttribute("asp-route-id", session.Id.ToString());

                TagBuilder div
            }
        }*/

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
