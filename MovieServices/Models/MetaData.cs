using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieServices.Models
{
    public class MetaData
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }

        public static MetaData ParseRow(string row)
        {
            var columns = row.Split(',');
            return new MetaData()
            {
                Id = int.Parse(columns[0]),
                MovieId = int.Parse(columns[1]),
                Title = columns[2],
                Language = columns[3],
                Duration = columns[4],
                ReleaseYear = int.Parse(columns[5])
            };
        }
    }
}
