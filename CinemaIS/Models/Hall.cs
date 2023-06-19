using CinemaIS.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CinemaIS.Models
{
    public class Hall
    {
        public int Id { get; set; }


        [DataType(DataType.MultilineText)]
        public string Schema { get; set; } = string.Empty;

        
    }
}
