using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CinemaIS.Models
{
    public class Movie
    {
        public int Id { get; set; }


        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;


        [Display(Name = "Жанры (разделять запятыми без пробелов)")]
        public string Genres { get; set; } = string.Empty;


        [Display(Name = "Страны (разделять запятыми без пробелов)")]
        public string Countries { get; set; } = string.Empty;


        [Range(1, 300)]
        [Display(Name = "Продолжительность (мин)")]
        public int Duration { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Дата выхода в прокат")]
        public DateTime ReleaseDate { get; set; }


        [Range(0, 18)]
        [Display(Name = "Возрастное ограничение")]
        public int AgeLimit { get; set; }


        [Display(Name = "Описание")]
        public string? Description { get; set; } = string.Empty;


        [Display(Name = "Постер")]
        public string? PosterUrl { get; set; }


        [Display(Name = "Сеансы")]
        public ICollection<Session> Sessions { get; set; }


        public Movie()
        {
            Sessions = new List<Session>();
        }
    }
}
