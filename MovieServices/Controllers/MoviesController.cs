using Microsoft.AspNetCore.Mvc;
using MovieServices.BusinessService.Contracts;
using MovieServices.Dto;
using MovieServices.Models;
using System.Collections.Generic;
using System.Net;

namespace WebApplication1.Controllers
{

    /// <summary>
    /// This controller class provide different endpoint references to movie studio.
    /// </summary>
    public class MoviesController : ControllerBase
    {
        /// <summary>
        /// Injecting movie business service using DI.
        /// </summary>
        private readonly IMovieService _movieService = null;

        /// <summary>
        /// Constructor of Movies Controller.
        /// </summary>
        /// <param name="movieService"></param>
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get the movie metadata with input movie id.
        /// </summary>
        /// <param name="movieid"></param>
        /// <returns></returns>
        [HttpGet("metadata/{movieid}")]
        [ProducesResponseType(typeof(List<MetaDataResponseDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetMetaData([FromRoute] int movieid)
        {
            var result = _movieService.GetMetaData(movieid);
            return Ok(result);
        }

        /// <summary>
        /// Get the movie stats data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("movies/stats")]
        [ProducesResponseType(typeof(List<StatsResponseDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetStatsData()
        {
            var result = _movieService.GetStatsData();
            return Ok(result);
        }

        /// <summary>
        /// Saves movie metadata.
        /// </summary>
        /// <param name="metaData"></param>
        /// <returns></returns>
        [HttpPost("metadata")]
        [ProducesResponseType(typeof(MetaDataResponseDto), (int)HttpStatusCode.OK)]
        public IActionResult SaveMovieMetaData([FromBody] MetaData metaData)
        {
            var result = _movieService.SaveMetaData(metaData);
            return Ok(result);
        }
    }
}
