using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using System.Drawing.Drawing2D;

namespace MobileShopAPI.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();

        Task<Category> GetById(long id);

        Task<CategoryResponse> Add(CategoryViewModel cate);

        Task<CategoryResponse> Update(long id,CategoryViewModel cate);

        Task<CategoryResponse> Delete(long id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryResponse> Add(CategoryViewModel cate)
        {
            var _cate = new Category
            {
                Name = cate.Name,
                Description = cate.Description,
                ImageUrl = cate.ImageUrl,
                CreatedDate = null
            };
            _context.Add(_cate);
            await _context.SaveChangesAsync();

            return new CategoryResponse
            {
                Message = "New Category Added!",
                isSuccess = true
            };
        }

        public async Task<CategoryResponse> Delete(long id)
        {
            var cate = await _context.Categories.SingleOrDefaultAsync(br => br.Id == id);
            if (cate != null)
            {
                _context.Remove(cate);
                await _context.SaveChangesAsync();

                return new CategoryResponse
                {
                    Message = "This Category Deleted",
                    isSuccess = true
                };
            }

            return new CategoryResponse
            {
                Message = "Delete Fail",
                isSuccess = false
            };
        }

        public async Task<List<Category>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetById(long id)
        {
            var cate = await _context.Categories.FindAsync(id);
            if (cate != null)
            {
                return cate;
            }
            return null;
        }

        public async Task<CategoryResponse> Update(long id, CategoryViewModel cate)
        {
            var _cate = await _context.Categories.FindAsync(id);
            if(_cate == null)
                return new CategoryResponse
                {
                    Message = "Category not found!",
                    isSuccess = false
                };
            _cate.Name = cate.Name;
            _cate.Description = cate.Description;
            _cate.ImageUrl = cate.ImageUrl;
            _cate.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new CategoryResponse
            {
                Message = "This Category Updated!",
                isSuccess = true
            };
        }
    }
}
