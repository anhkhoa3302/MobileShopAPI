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
            var productList = await _productService.GetAllNoneHiddenProductAsync();
            return Ok(productList);
        }

        [HttpGet("AdminGetAll")]
        public async Task<IActionResult> AdminGetAll()
        {
            var productList = await _productService.GetAllProductAsync();
            return Ok(productList);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> ProductDetail(long id)
        {
            var product = await _productService.GetNoneHiddenProductDetailAsync(id);
            if(product == null) { return BadRequest("Product not found"); }
            return Ok(product);
        }
        [HttpGet("AdminGetById/{id}")]
        public async Task<IActionResult> AdminProductDetail(long id)
        {
            var product = await _productService.GetProductDetailAsync(id);
            if (product == null) { return BadRequest("Product not found"); }
            return Ok(product);
        }
        [HttpPost("add")]
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

        [HttpPost("ApproveProduct/{id}")]
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


        [HttpPut("update/{id}")]
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


        [HttpDelete("delete/{id}")]
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
