using Microsoft.AspNetCore.Hosting;
using MovieServices.BusinessService.Contracts;
using MovieServices.Dto;
using MovieServices.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieServices.BusinessService
{
    /// <summary>
    /// Movie business service class.
    /// </summary>
    public class MovieBusinessService : IMovieService
    {
        /// <summary>
        /// List for saving the data.
        /// </summary>
        List<MetaData> database = null;

        /// <summary>
        /// Web host environment variable
        /// </summary>
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Contructor for MovieBusinessService
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public MovieBusinessService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Getting movie data.
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public List<MetaDataResponseDto> GetMetaData(int movieId)
        {
            var result = GetMetaDataCSV().Where(x => x.MovieId == movieId && CheckValidData(x))
                                         .GroupBy(o => o.Language)
                                         .Select(o => o.FirstOrDefault());

            if (result.Any())
            {
                return result.Select(x => new MetaDataResponseDto
                {
                    MovieId = x.MovieId,
                    Title = x.Title,
                    Language = x.Language,
                    Duration = x.Duration,
                    ReleaseYear = x.ReleaseYear
                }).OrderBy(x => x.Language).ToList();
            }

            else
            {
                throw new MovieException(ExceptionCode.NotFound, "MovieId not found");
            }
        }

        /// <summary>
        /// Get stats data
        /// </summary>
        /// <returns></returns>
        public List<StatsResponseDto> GetStatsData()
        {
            var metaDataCSV = GetMetaDataCSV().GroupBy(x => x.MovieId).Select(y => y.First()).ToList();
            var statDataCSV = GetStatsDataCSV().OrderByDescending(s => s.AverageWatchDuration)
                                               .GroupBy(x => x.MovieId)
                                               .Select(o => new
                                               {
                                                   MovieId = o.Key,
                                                   Count = o.Count()
                                               });

            var result = (from stat in statDataCSV
                          join meta in metaDataCSV on stat.MovieId equals meta.MovieId
                          select new StatsResponseDto { MovieId = stat.MovieId, Title = meta.Title, Watches = stat.Count, ReleaseYear = meta.ReleaseYear });

            return result.OrderByDescending(x => x.Watches).ThenByDescending(y => y.ReleaseYear).ToList();
        }

        /// <summary>
        /// Save meta data of movie.
        /// </summary>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public MetaDataResponseDto SaveMetaData(MetaData metaData)
        {
            var result = AddMovieMetaData(metaData);
            if (result)
            {
                return new MetaDataResponseDto()
                {
                    MovieId = metaData.MovieId,
                    Title = metaData.Title,
                    Language = metaData.Language,
                    Duration = metaData.Duration,
                    ReleaseYear = metaData.ReleaseYear
                };
            }
            else
            {
                throw new MovieException(ExceptionCode.NotFound, "Unable to save the record");
            }
        }

        /// <summary>
        /// Add movie meta data to database list.
        /// </summary>
        /// <param name="metaData"></param>
        /// <returns></returns>
        private bool AddMovieMetaData(MetaData metaData)
        {
            database = new List<MetaData>
            {
                  new MetaData
                  {
                    MovieId = metaData.MovieId,
                    Title = metaData.Title,
                    Language = metaData.Language,
                    Duration = metaData.Duration,
                    ReleaseYear = metaData.ReleaseYear
                  }
            };
            return database.Any();
        }

        /// <summary>
        /// Check validity of data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckValidData(MetaData data)
        {
            return (!string.IsNullOrEmpty(data.Language)) &&
                   (!string.IsNullOrEmpty(data.Title)) &&
                   (!string.IsNullOrEmpty(data.Duration)) &&
                   (data.ReleaseYear > 0);
        }

        /// <summary>
        /// Get MetaData from CSV file.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<MetaData> GetMetaDataCSV()
        {
            var path = _hostingEnvironment.ContentRootPath;
            var @formatPath = path + MoviesConstant.MetaDataPath;
            return File.ReadAllLines(@formatPath)
                       .Skip(1)
                       .Where(line => line.Length > 0)
                       .Select(MetaData.ParseRow);
        }

        /// <summary>
        /// Get stats data from CSV file.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<StatsData> GetStatsDataCSV()
        {
            var path = _hostingEnvironment.ContentRootPath;
            var @formatPath = path + MoviesConstant.StatsDataPath;

            return File.ReadAllLines(@formatPath)
                       .Skip(1)
                       .Where(line => line.Length > 0)
                       .Select(StatsData.ParseRow);
        }
    }
}
