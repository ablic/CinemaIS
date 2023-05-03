using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CinemaIS.Models
{
    public class Movie
    {
        public int Id { get; set; }


        [Display(Name = "Название")]
        public string Name { get; set; } = "[NAMELESS]";


        [Display(Name = "Жанры")]
        public ICollection<Genre> Genres { get; set; }


        [Display(Name = "Страны")]
        public ICollection<Country> Countries { get; set; }


        [Range(1, 300)]
        [Display(Name = "Продолжительность (мин)")]
        public int Duration { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Дата выхода")]
        public DateTime ReleaseDate { get; set; }


        [Precision(3, 1)]
        [Display(Name = "Рейтинг")]
        public decimal? Rating { get; set; }


        [Display(Name = "Описание")]
        public string? Description { get; set; } = string.Empty;


        [Display(Name = "Постер")]
        public string? PosterUrl { get; set; }


        [Display(Name = "Сеансы")]
        public ICollection<Session> Sessions { get; set; }


        public Movie()
        {
            Genres = new List<Genre>();
            Countries = new List<Country>();
            Sessions = new List<Session>();
        }
    }
}
