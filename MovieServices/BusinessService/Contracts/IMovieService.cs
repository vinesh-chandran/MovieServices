using MovieServices.Dto;
using MovieServices.Models;
using System.Collections.Generic;

namespace MovieServices.BusinessService.Contracts
{

    /// <summary>
    /// Interface for movie business service.
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Saves the meta data.
        /// </summary>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public MetaDataResponseDto SaveMetaData(MetaData metaData);

        /// <summary>
        /// Getting the meta data.
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public List<MetaDataResponseDto> GetMetaData(int movieId);

        /// <summary>
        /// Listing stats data
        /// </summary>
        /// <returns></returns>
        public List<StatsResponseDto> GetStatsData();
    }
}
