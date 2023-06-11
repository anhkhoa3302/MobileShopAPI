using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Request;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ProductCreateRequest request)
        {
            var dbProduct = await _context.Products.FindAsync(request.Id);
            if (dbProduct != null)
                return BadRequest($"Function with id {request.Id} is existed.");
            var product = new Product()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Stock = request.Stock,
                Status = request.Status,
                UserId = request.UserId,
                SizeId = request.SizeId,
                ColorId = request.ColorId,
            };
            _context.Products.Add(product);
            var result = await _context.SaveChangesAsync();
            if(result > 0)
            {
                return CreatedAtAction(nameof(GetById), new {id = product.Id}, request);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = _context.Products;

            var productvm = await products.Select(u => new ProductViewModel()
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                Stock = u.Stock,
                Status = u.Status,
                UserId = u.UserId,
                SizeId = u.SizeId,
                ColorId = u.ColorId
            }).ToListAsync();

            return Ok(productvm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            var productvm = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Status = product.Status,
                UserId = product.UserId,
                SizeId = product.SizeId,
                ColorId = product.ColorId,
            };
            return Ok(productvm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, [FromBody] ProductCreateRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            product.Name = request.Name;
            product.Description = request.Description;
            product.Stock = request.Stock;
            product.Status = request.Status;
            product.SizeId = request.SizeId;
            product.ColorId = request.ColorId;
            

            _context.Products.Update(product);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var productvm = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Stock = product.Stock,
                    Status = product.Status,
                    SizeId = product.SizeId,
                    ColorId = product.ColorId,
                    UserId = product.UserId
                };
                return Ok(productvm);
            }
            return BadRequest();
        }
    }
}
