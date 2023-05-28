using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using System.Drawing.Drawing2D;

namespace MobileShopAPI.Services
{
    public interface ICategoryService
    {
        List<Category> GetAll();

        Category GetById(long id);

        Task<CategoryResponse> Add(Category cate);

        Task<CategoryResponse> Update(Category cate);

        Task<CategoryResponse> Delete(long id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryResponse> Add(Category cate)
        {
            var _cate = new Category
            {
                Name = cate.Name,
                Description = cate.Description,
                ImageUrl = cate.ImageUrl,
                CreatedDate = null
            };
            _context.Add(_cate);
            _context.SaveChanges();

            return new CategoryResponse
            {
                Message = "New Category Added!",
                isSuccess = true
            };
            //return new Category
            //{
            //    Id = _cate.Id,
            //    Name = _cate.Name,
            //    Description = _cate.Description,
            //    ImageUrl = _cate.ImageUrl,
            //    CreatedDate = _cate.CreatedDate
            //};
        }

        public async Task<CategoryResponse> Delete(long id)
        {
            var cate = _context.Categories.SingleOrDefault(br => br.Id == id);
            if (cate != null)
            {
                _context.Remove(cate);
                _context.SaveChanges();

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

        public List<Category> GetAll()
        {
            var cates = _context.Categories.Select(br => new Category
            {
                Id = br.Id,
                Name = br.Name,
                Description = br.Description,
                ImageUrl = br.ImageUrl,
                CreatedDate = br.CreatedDate
            });
            return cates.ToList();
        }

        public Category GetById(long id)
        {
            var cate = _context.Categories.SingleOrDefault(b => b.Id == id);
            if (cate != null)
            {
                return new Category
                {
                    Id = cate.Id,
                    Name = cate.Name,
                    Description = cate.Description,
                    ImageUrl = cate.ImageUrl,
                    CreatedDate = cate.CreatedDate
                };
            }
            return null;
        }

        public async Task<CategoryResponse> Update(Category cate)
        {
            var _cate = _context.Categories.SingleOrDefault(br => br.Id == cate.Id);
            _cate.Name = cate.Name;
            _cate.Description = cate.Description;
            _cate.ImageUrl = cate.ImageUrl;
            _cate.CreatedDate = cate.CreatedDate;
            _context.SaveChanges();

            return new CategoryResponse
            {
                Message = "This Category Updated!",
                isSuccess = true
            };
        }
    }
}
