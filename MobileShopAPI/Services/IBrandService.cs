using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;

namespace MobileShopAPI.Services
{
    public interface IBrandService
    {
        List<BrandViewModelcs> GetAll();

        BrandViewModelcs GetById(long id);

        BrandViewModelcs Add(Brand brand);

        void Update(BrandViewModelcs brand);

        void Delete(long id);
    }

    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;

        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }
        public BrandViewModelcs Add(Brand brand)
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

            return new BrandViewModelcs
            {
                Id = _brand.Id,
                Name = _brand.Name,
                Description = _brand.Description,
                ImageUrl = _brand.ImageUrl,
                CreatedDate = _brand.CreatedDate
            };

            //_context.Brands.Add(brand);
            //_context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);

        }

        public void Delete(long id)
        {
            var brand = _context.Brands.SingleOrDefault(br => br.Id == id);
            if(brand != null)
            {
                _context.Remove(brand);
                _context.SaveChanges();
            }
        }

        public List<BrandViewModelcs> GetAll()
        {
            var brands = _context.Brands.Select(br => new BrandViewModelcs
            {
                Id = br.Id,
                Name = br.Name,
                Description = br.Description,
                ImageUrl = br.ImageUrl,
                CreatedDate = br.CreatedDate
            });
            return brands.ToList();
        }

        public BrandViewModelcs GetById(long id)
        {
            var brand = _context.Brands.SingleOrDefault(b => b.Id == id);
            if(brand != null)
            {
                return new BrandViewModelcs
                {
                    Name = brand.Name,
                    Description = brand.Description,
                    ImageUrl = brand.ImageUrl,
                    CreatedDate = brand.CreatedDate
                };
            }
            return null;
        }

        public void Update(BrandViewModelcs brand)
        {
            var _brand = _context.Brands.SingleOrDefault(br => br.Id == brand.Id);
            _brand.Name = brand.Name;
            _brand.Description = brand.Description;
            _brand.ImageUrl = brand.ImageUrl;
            _brand.CreatedDate = brand.CreatedDate;
            _context.SaveChanges();
        }
    }
}
