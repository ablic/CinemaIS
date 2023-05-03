using System.ComponentModel.DataAnnotations;

namespace CinemaIS.Models
{
    public class Hall
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        public string Schema { get; set; } =
            "xxxxxxxxxx\n" +
            "xxxxxxxxxx\n" +
            "xxxxxxxxxx\n" +
            "xxxxxxxxxx\n" +
            "xxxxxxxxxx\n" +
            "xxxxxxxxxx\n" +
            "xxxxxxxxxx\n" +
            "xxxxxxxxxx\n" +
            "xxxxxxxxxx\n" +
            "xxxxxxxxxx\n";
    }
}
