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
            var productList = await _productService.GetAllProductWithCoverAsync();
            return Ok(productList);
        }
        [HttpPost("createProduct")]
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
    }
}
