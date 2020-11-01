namespace MovieServices.Models
{
    public class StatsData
    {
        public int MovieId { get; set; }
        public int AverageWatchDuration { get; set; }

        public static StatsData ParseRow(string row)
        {
            var columns = row.Split(',');
            return new StatsData()
            {
                MovieId = int.Parse(columns[0]),
                AverageWatchDuration = int.Parse(columns[1])
            };
        }
    }
}
