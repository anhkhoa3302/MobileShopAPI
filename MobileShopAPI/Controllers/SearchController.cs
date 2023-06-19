using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        /// <summary>
        /// Search product
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Search all:
        /// 
        ///     GET
        ///     {
        ///         "keyWord": "string",
        ///         "categoryId": 0,
        ///         "brandId": 0
        ///     }
        /// 
        /// Search by brand:
        ///     
        ///     GET
        ///     {
        ///         "keyWord": "string",
        ///         "categoryId": 0,
        ///         "brandId": {brandId}
        ///     }
        ///     
        /// Search by category:
        /// 
        ///     GET
        ///     {
        ///         "keyWord": "string",
        ///         "categoryId": {categoryId},
        ///         "brandId": 0
        ///     }
        ///     
        /// 
        /// Search by brand and category:
        /// 
        ///     GET
        ///     {
        ///         "keyWord": "string",
        ///         "categoryId": {categoryId},
        ///         "brandId": {brandId}
        ///     }
        /// 
        /// </remarks>
        /// <response code ="200">Search result</response>
        /// <response code ="400">Not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(typeof(List<Product>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(SearchViewModel model)
        {
            if (model.CategoryId != 0 && model.BrandId != 0)
            {
                var result = await _searchService.SearchProductByBrandAndCategory(model);
                if(result!=null)
                    return Ok(result);
                return BadRequest("Not found");
            }
            else if (model.CategoryId == 0 && model.BrandId != 0)
            {
                var result = await _searchService.SearchProductByBrand(model);
                if (result != null)
                    return Ok(result);
                return BadRequest("Not found");
            }
            else if (model.CategoryId != 0 && model.BrandId == 0)
            {
                var result = await _searchService.SearchProductByCategory(model);
                if (result != null)
                    return Ok(result);
                return BadRequest("Not found");
            }
            else
            {
                var result = await _searchService.SearchAllProduct(model);
                if (result != null)
                    return Ok(result);
                return BadRequest("Not found");
            }
        }
    }
}
