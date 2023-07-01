using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Get all posts by Category in Day
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByCategoryInDay")]
        public async Task<IActionResult> getPostsByCategoryInDay()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByCategoryInDay());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by Category in Week
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByCategoryInWeek")]
        public async Task<IActionResult> getPostsByCategoryInWeek()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByCategoryInWeek());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by Category in Month
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByCategoryInMonth")]
        public async Task<IActionResult> getPostsByCategoryInMonth()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByCategoryInMonth());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Get all posts by Category in stages
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByCategoryInStages")]
        public async Task<IActionResult> getPostsByCategoryInStages(PeriodViewModel model)
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByCategoryInStages(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by User in Day
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByUserInDay")]
        public async Task<IActionResult> getPostsByUserInDay()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByUserInDay());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by User in Week
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByUserInWeek")]
        public async Task<IActionResult> getPostsByUserInWeek()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByUserInWeek());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all posts by User in Month
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByUserInMonth")]
        public async Task<IActionResult> getPostsByUserInMonth()
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByUserInMonth());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Get all posts by User in stages
        /// </summary>
        /// <response code ="200">Get all posts</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getPostsByUserInStages")]
        public async Task<IActionResult> getPostsByUserInStages(PeriodViewModel model)
        {
            try
            {
                return Ok(await _statisticsService.GetPostsByUserInStages(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all Users by Post amount in stages
        /// </summary>
        /// <response code ="200">Get all user</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getUserByPostInStages")]
        public async Task<IActionResult> getUserByPostInStages(PeriodViewModel model)
        {
            try
            {
                return Ok(await _statisticsService.GetUserByPostInStages(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all Users by Package Purchases amount in stages
        /// </summary>
        /// <response code ="200">Get all user</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getUserByPackagePurchasesInStages")]
        public async Task<IActionResult> getUserByPackagePurchasesInStages(PeriodViewModel model)
        {
            try
            {
                return Ok(await _statisticsService.GetUserByPackagePurchasesInStages(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
