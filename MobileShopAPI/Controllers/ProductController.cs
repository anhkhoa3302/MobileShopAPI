using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll() 
        {
            var productList = await _productService.GetAllProductAsync();
            return Ok(productList);
        }

        [HttpGet("getDetail/{productId}")]
        public async Task<IActionResult> ProductDetail(long productId)
        {
            var product = await _productService.GetProductDetailAsync(productId);
            if(product == null) { return BadRequest("Product not found"); }
            return Ok(product);
        }
        [HttpPost("create")]
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

        [HttpPut("edit/{productId}")]
        public async Task<IActionResult> Edit(long productId,ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProductAsync(productId,model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }


        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> Delete(long productId)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.DeleteProductAsync(productId);
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
