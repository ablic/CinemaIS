using CinemaIS.Models;
using System.Collections.ObjectModel;

namespace CinemaIS.ViewModels
{
    public class SeatSelectionInfo
    {
        public decimal MinPrice { get; set; }
        public bool[][] SeatOccupancy { get; set; }
        public bool[][] SeatSelections { get; set; }
    }
}
