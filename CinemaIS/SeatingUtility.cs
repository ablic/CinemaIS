using CinemaIS.Models;
using CinemaIS.ViewModels;

namespace CinemaIS
{
    public static class SeatingUtility
    {
        public static string GetPlace(int rowIndex, int seatIndex)
        {
            return $"{rowIndex + 1}-{seatIndex + 1}";
        }

        public static async Task<Seat[][]> GetSeatsAsync(Session session)
        {
            string[] rows = session.Hall.Schema.Split('\n');
            Seat[][] layout = new Seat[rows.Length][];

            for (int i = 0; i < rows.Length; i++)
            {
                string[] elements = rows[i].Replace("\r", "").Split(' ');
                layout[i] = new Seat[elements.Length];

                for (int j = 0; j < elements.Length; j++)
                {
                    string[] elementWithGaps = elements[j].Split('-');

                    switch (elementWithGaps.Length)
                    {
                        case 1:
                            layout[i][j] = new Seat
                            {
                                LeftGap = 0f,
                                RightGap = 0f,
                                Price = decimal.Parse(elementWithGaps[0]) * session.MinPrice
                            };
                            break;

                        case 2:
                            layout[i][j] = new Seat
                            {
                                LeftGap = float.Parse(elementWithGaps[0]),
                                RightGap = 0f,
                                Price = decimal.Parse(elementWithGaps[1]) * session.MinPrice
                            };
                            break;

                        case 3:
                            layout[i][j] = new Seat
                            {
                                LeftGap = float.Parse(elementWithGaps[0]),
                                RightGap = float.Parse(elementWithGaps[2]),
                                Price = decimal.Parse(elementWithGaps[1]) * session.MinPrice
                            };
                            break;
                    }
                }
            }

            return layout;
        }
    }
}
