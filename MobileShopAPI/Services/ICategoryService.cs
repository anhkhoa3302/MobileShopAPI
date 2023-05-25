using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using System.Drawing.Drawing2D;

namespace MobileShopAPI.Services
{
    public interface ICategoryService
    {
        List<CategogyViewModels> GetAll();

        CategogyViewModels GetById(long id);

        CategogyViewModels Add(Category cate);

        void Update(CategogyViewModels cate);

        void Delete(long id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public CategogyViewModels Add(Category cate)
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

            return new CategogyViewModels
            {
                Id = _cate.Id,
                Name = _cate.Name,
                Description = _cate.Description,
                ImageUrl = _cate.ImageUrl,
                CreatedDate = _cate.CreatedDate
            };
        }

        public void Delete(long id)
        {
            var cate = _context.Categories.SingleOrDefault(br => br.Id == id);
            if (cate != null)
            {
                _context.Remove(cate);
                _context.SaveChanges();
            }
        }

        public List<CategogyViewModels> GetAll()
        {
            var cates = _context.Categories.Select(br => new CategogyViewModels
            {
                Id = br.Id,
                Name = br.Name,
                Description = br.Description,
                ImageUrl = br.ImageUrl,
                CreatedDate = br.CreatedDate
            });
            return cates.ToList();
        }

        public CategogyViewModels GetById(long id)
        {
            var cate = _context.Categories.SingleOrDefault(b => b.Id == id);
            if (cate != null)
            {
                return new CategogyViewModels
                {
                    Name = cate.Name,
                    Description = cate.Description,
                    ImageUrl = cate.ImageUrl,
                    CreatedDate = cate.CreatedDate
                };
            }
            return null;
        }

        public void Update(CategogyViewModels cate)
        {
            var _cate = _context.Categories.SingleOrDefault(br => br.Id == cate.Id);
            _cate.Name = cate.Name;
            _cate.Description = cate.Description;
            _cate.ImageUrl = cate.ImageUrl;
            _cate.CreatedDate = cate.CreatedDate;
            _context.SaveChanges();
        }
    }
}
