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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// Get all non-hidden product
        /// </summary>
        /// <remarks>Product is hidden if "Status = 2 , 3" or "isHidden = true"</remarks>
        /// <response code ="200">Get all non-hidden product</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll() 
        {
            var productList = await _productService.GetAllNoneHiddenProductAsync();
            return Ok(productList);
        }
        /// <summary>
        /// Get all product
        /// </summary>
        /// <remarks>Only admin can access this API</remarks>
        /// <response code ="200">Get all product</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("AdminGetAll")]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AdminGetAll()
        {
            var productList = await _productService.GetAllProductAsync();
            return Ok(productList);
        }
        /// <summary>
        /// Get non-hidden product detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Product is hidden if "Status = 2 , 3" or "isHidden = true"</remarks>
        /// <returns></returns>
        /// <response code ="200">Product infos</response>
        /// <response code ="400">Product not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("getById/{id}")]
        [ProducesResponseType(typeof(ProductDetailViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ProductDetail(long id)
        {
            var product = await _productService.GetNoneHiddenProductDetailAsync(id);
            if(product == null) { return BadRequest("Product not found"); }
            return Ok(product);
        }
        /// <summary>
        /// Get product detail
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Only admin can access this API</remarks>
        /// <returns></returns>
        /// <response code ="200">Product infos</response>
        /// <response code ="400">Product not found</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpGet("AdminGetById/{id}")]
        [ProducesResponseType(typeof(ProductDetailViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AdminProductDetail(long id)
        {
            var product = await _productService.GetProductDetailAsync(id);
            if (product == null) { return BadRequest("Product not found"); }
            return Ok(product);
        }
        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Product has been added successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ProductResponse),400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _productService.CreateProductAsync(model);
                if(result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        /// <summary>
        /// Approve product
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Approve product</remarks>
        /// <returns></returns>
        /// <response code ="200">Product has been approved</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPost("ApproveProduct/{id}")]
                [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ProductResponse),400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ApproveProduct(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.ApproveProduct(id);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        /// <summary>
        /// update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code ="200">Product has been updated successfully</response>
        /// <response code ="400">Model has missing/invalid values</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ProductResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Edit(long id,ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProductAsync(id,model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// If product already had an order and transaction
        /// it will be soft deleted by setting it status to 3
        /// </remarks>
        /// <returns></returns>
        /// <response code ="200">Product has been deleted successfully</response>
        /// <response code ="400">Something went wrong</response>
        /// <response code ="500">>Oops! Something went wrong</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ProductResponse), 200)]
        [ProducesResponseType(typeof(ProductResponse), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.DeleteProductAsync(id);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
