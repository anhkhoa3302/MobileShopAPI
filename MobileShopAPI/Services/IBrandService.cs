using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;

namespace MobileShopAPI.Services
{
    public interface IBrandService
    {
        List<Brand> GetAll();

        Brand GetById(long id);

        Task<BrandResponse> Add(Brand brand);

        Task<BrandResponse> Update(Brand brand);

        Task<BrandResponse> Delete(long id);
    }

    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;

        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BrandResponse> Add(Brand brand)
        {
            var _brand = new Brand
            {
                Name = brand.Name,
                Description = brand.Description,
                ImageUrl = brand.ImageUrl,
                CreatedDate = null
            };
            _context.Add(_brand);
            _context.SaveChanges();

            return new BrandResponse
            {
                Message = "New Brand Added!",
                isSuccess = true
            };
            //return new Brand
            //{
            //    Id = _brand.Id,
            //    Name = _brand.Name,
            //    Description = _brand.Description,
            //    ImageUrl = _brand.ImageUrl,
            //    CreatedDate = _brand.CreatedDate
            //};

            //_context.Brands.Add(brand);
            //_context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);

        }

        public async Task<BrandResponse> Delete(long id)
        {
            var brand = _context.Brands.SingleOrDefault(br => br.Id == id);
            if(brand != null)
            {
                _context.Remove(brand);
                _context.SaveChanges();

                return new BrandResponse
                {
                    Message = "This Brand Deleted",
                    isSuccess = true
                };
            }

            return new BrandResponse
            {
                Message = "Delete Fail !!!",
                isSuccess = false
            };
        }

        public List<Brand> GetAll()
        {
            var brands = _context.Brands.Select(br => new Brand
            {
                Id = br.Id,
                Name = br.Name,
                Description = br.Description,
                ImageUrl = br.ImageUrl,
                CreatedDate = br.CreatedDate
            });
            return brands.ToList();
        }

        public Brand GetById(long id)
        {
            var brand = _context.Brands.SingleOrDefault(b => b.Id == id);
            if(brand != null)
            {
                return new Brand
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Description = brand.Description,
                    ImageUrl = brand.ImageUrl,
                    CreatedDate = brand.CreatedDate
                };
            }
            return null;
        }

        public async Task<BrandResponse> Update(Brand brand)
        {
            var _brand = _context.Brands.SingleOrDefault(br => br.Id == brand.Id);
            _brand.Name = brand.Name;
            _brand.Description = brand.Description;
            _brand.ImageUrl = brand.ImageUrl;
            _brand.CreatedDate = brand.CreatedDate;
            _context.SaveChanges();

            return new BrandResponse
            {
                Message = "This Brand Updated!",
                isSuccess = true
            };
        }
    }
}
