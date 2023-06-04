using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductAsync();

        Task<Product?> GetProductDetailAsync(long productId);

        Task<ProductResponse> CreateProductAsync(ProductViewModel model);

        Task<ProductResponse> EditProductAsync(long productId,ProductViewModel model);

        Task<ProductResponse> DeleteProductAsync(long productId);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public ProductService(ApplicationDbContext context,IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductViewModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Stock = model.Stock,
                Price = model.Price,
                Status = model.Status,
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                UserId = model.UserId,
                SizeId = model.SizeId,
                ColorId = model.ColorId
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            bool hasCover = false;
            if(model.Images != null)
                foreach(var item in model.Images)
                {
                    var image = new ImageViewModel();
                    if(!hasCover)
                    {

                        image.IsCover = true;
                        hasCover = true;
                    }
                    else
                    {
                        image.IsCover = false;
                    }
                    image.Url = item.Url;
                    await _imageService.AddAsync(product.Id, image);
                }

            return new ProductResponse
            {
                Message = "Product has been created successfully",
                isSuccess = true
            };
        }

        public async Task<ProductResponse> DeleteProductAsync(long productId)
        {
            var product = await _context.Products.Where(p => p.Id == productId && p.Status != 3)
                .Include(p => p.ProductOrders)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return new ProductResponse
                {
                    Message = "Product not found!",
                    isSuccess = false
                };
            }
            if(product.ProductOrders.Any())
            {
                product.Status = 3;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return new ProductResponse
                {
                    Message = "Product has been soft deleted!",
                    isSuccess = true
                };
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new ProductResponse
            {
                Message = "Product has been deleted!",
                isSuccess = true
            };

        }

        public async Task<ProductResponse> EditProductAsync(long productId,ProductViewModel model)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return new ProductResponse
                {
                    Message = "Product not found",
                    isSuccess = false
                };
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Status = model.Status;
            product.CategoryId = model.CategoryId;
            product.BrandId = model.BrandId;
            product.UserId = model.UserId;
            product.SizeId = model.SizeId;
            product.ColorId = model.ColorId;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            if (model.Images != null)
                foreach (var item in model.Images)
                {
                    if(item.IsDeleted)
                    {
                        await _imageService.DeleteAsync(item.Id);
                        continue;
                    }
                    if(item.IsNewlyAdded)
                    {
                        await _imageService.AddAsync(product.Id,item);
                        continue;
                    }
                    await _imageService.UpdateAsync(item);
                }
            return new ProductResponse
            {
                Message = "Product has been updated successfully",
                isSuccess = true
            };
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            var productList = await _context.Products.AsNoTracking()
                .Include(p => p.Images)
                .Include(p=>p.Brand)
                .Include(p => p.Category)
                .ToListAsync();

            return productList;
        }

        public async Task<Product?> GetProductDetailAsync(long productId)
        {
            var product = await _context.Products.AsNoTracking().Where(p=>p.Id == productId)
                .Include(p => p.Images)
                .Include(p => p.Brand)
                .Include(p=>p.Category)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .Include(p => p.User)
                .SingleOrDefaultAsync();
            if (product == null) return null;
            return product;
        }
    }
}
