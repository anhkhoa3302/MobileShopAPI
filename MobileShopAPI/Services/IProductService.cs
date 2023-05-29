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
        Task<List<ProductViewModel>> GetAllProductWithCoverAsync();
        //Task<ProductViewModel> GetProductDetailAsync(string productId);

        Task<ProductResponse> CreateProductAsync(ProductViewModel model);
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
            foreach(var item in model.Images)
            {
                var image = new ImageViewModel();
                if (hasCover)
                {
                    image.IsCover = false;
                }
                if ((bool)item.IsCover)
                {
                    hasCover = true;
                }
                image.Url = item.Url;
                await _imageService.AddImageAsync(product.Id, image);
            }

            return new ProductResponse
            {
                Message = "Product has been created successfully",
                isSuccess = true
            };
        }

        public async Task<List<ProductViewModel>> GetAllProductWithCoverAsync()
        {
            var productList = await _context.Products.Include(p => p.Images).ToListAsync();
            List<ProductViewModel> result = new List<ProductViewModel>();


            foreach (var item in productList)
            {
                var temp = new ProductViewModel
                {
                    Name = item.Name,
                    Description = item.Description,
                    Stock = item.Stock,
                    Price = item.Price,
                    Status = item.Status,
                    CategoryId = item.CategoryId,
                    BrandId = item.BrandId,
                    UserId = item.UserId,
                    SizeId = item.SizeId,
                    ColorId = item.ColorId
                };
                foreach(var image in item.Images)
                {
                    var productImg = new ImageViewModel
                    {
                        Url = image.Url,
                        IsCover = image.IsCover
                    };
                    temp.Images.Add(productImg);
                }
                result.Add(temp);
            }
            return result;
        }

        //public async Task<ProductViewModel> GetProductDetailAsync(string productId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
