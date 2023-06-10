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

        Task<List<Product>> GetAllNoneHiddenProductAsync();

        Task<ProductResponse> ApproveProduct(long productId);

        Task<ProductDetailViewModel?> GetProductDetailAsync(long productId);

        Task<ProductDetailViewModel?> GetNoneHiddenProductDetailAsync(long productId);

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

        public async Task<ProductResponse> ApproveProduct(long productId)
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
            product.isHidden = false;
            if(product.Stock <= 0) 
            { 
                product.Stock = 0;
                product.Status = 1;
            }
            else
            {
                product.Status = 0;
            }
            await _context.SaveChangesAsync();
            return new ProductResponse
            {
                Message = "Product has been approved",
                isSuccess = true
            };
        }

        public async Task<ProductResponse> CreateProductAsync(ProductViewModel model)
        {
            if (model == null)
                return new ProductResponse
                {
                    Message = "Model is null",
                    isSuccess = false
                };
            if(model.Images == null)
                return new ProductResponse
                {
                    Message = "Images list is null",
                    isSuccess = false
                };
            if (model.Images.Count < 2)
            {
                return new ProductResponse
                {
                    Message = "Product need at least 2 image",
                    isSuccess = false
                };
            }    
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
            var product = await _context.Products
                .Where(p=>p.Id == productId && p.Status != 3)
                .SingleOrDefaultAsync();
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
            product.CategoryId = model.CategoryId;
            product.BrandId = model.BrandId;
            product.SizeId = model.SizeId;
            product.ColorId = model.ColorId;
            product.isHidden = model.isHidden;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            if (model.Images != null)
                foreach (var item in model.Images)
                {
                    if(item.IsDeleted)
                    {
                        await _imageService.DeleteAsync(productId, item.Id);
                        continue;
                    }
                    if(item.IsNewlyAdded)
                    {
                        await _imageService.AddAsync(productId, item);
                        continue;
                    }
                    await _imageService.UpdateAsync(productId,item);
                }
            await _imageService.CheckCover(productId);
            return new ProductResponse
            {
                Message = "Product has been updated successfully",
                isSuccess = true
            };
        }

        public async Task<List<Product>> GetAllNoneHiddenProductAsync()
        {
            var productList = await _context.Products.AsNoTracking()
                .Where(p=>p.isHidden == false && (p.Status == 0 || p.Status == 1))
                .Include(p => p.Images)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();

            return productList;
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

        public async Task<ProductDetailViewModel?> GetNoneHiddenProductDetailAsync(long productId)
        {
            var product = await _context.Products.AsNoTracking().Where(p => p.Id == productId)
                .Where(p => p.isHidden == false && (p.Status == 0 || p.Status == 1))
                .Include(p => p.Images)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .SingleOrDefaultAsync();
            if (product == null) return null;

            var user = await _context.Users.Where(u => u.Id == product.UserId).SingleOrDefaultAsync();
            if (user == null) return null;

            var userView = new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.LastName,
                LastName = user.LastName,
                Description = user.Description,
                Status = user.Status,
                AvatarUrl = user.AvatarUrl,
                UserBalance = user.UserBalance,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };

            var returnValue = new ProductDetailViewModel
            {
                Product = product,
                User = userView
            };


            return returnValue;
        }

        public async Task<ProductDetailViewModel?> GetProductDetailAsync(long productId)
        {
            var product = await _context.Products.AsNoTracking().Where(p=>p.Id == productId)
                .Include(p => p.Images)
                .Include(p => p.Brand)
                .Include(p=>p.Category)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .SingleOrDefaultAsync();
            if (product == null) return null;

            var user = await _context.Users.Where(u=>u.Id == product.UserId).SingleOrDefaultAsync();
            if(user == null) return null;

            var userView = new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.LastName,
                LastName = user.LastName,
                Description = user.Description,
                Status = user.Status,
                AvatarUrl = user.AvatarUrl,
                UserBalance = user.UserBalance,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };

            var returnValue = new ProductDetailViewModel
            {
                Product = product,
                User = userView
            };


            return returnValue;
        }
    }
}
