using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using System.Drawing.Drawing2D;

namespace MobileShopAPI.Services
{
    public interface ISizeService
    {
        List<SizeViewModels> GetAll();

        SizeViewModels GetById(long id);

        SizeViewModels Add(Size size);

        void Update(SizeViewModels size);

        void Delete(long id);

        public class SizeService : ISizeService
        {
            private readonly ApplicationDbContext _context;

            public SizeService(ApplicationDbContext context)
            {
                _context = context;
            }
            public SizeViewModels Add(Size size)
            {
                var _size = new Size
                {
                    SizeName = size.SizeName,
                    CreatedDate = null
                };
                _context.Add(_size);
                _context.SaveChanges();

                return new SizeViewModels
                {
                    Id = _size.Id,
                    SizeName = _size.SizeName,
                    CreatedDate = _size.CreatedDate
                };
            }

            public void Delete(long id)
            {
                var size = _context.Sizes.SingleOrDefault(br => br.Id == id);
                if (size != null)
                {
                    _context.Remove(size);
                    _context.SaveChanges();
                }
            }

            public List<SizeViewModels> GetAll()
            {
                var sizes = _context.Sizes.Select(br => new SizeViewModels
                {
                    Id = br.Id,
                    SizeName = br.SizeName,
                    CreatedDate = br.CreatedDate
                });
                return sizes.ToList();
            }

            public SizeViewModels GetById(long id)
            {
                var size = _context.Sizes.SingleOrDefault(b => b.Id == id);
                if (size != null)
                {
                    return new SizeViewModels
                    {
                        Id = size.Id,
                        SizeName = size.SizeName,
                        CreatedDate = size.CreatedDate
                    };
                }
                return null;
            }

            public void Update(SizeViewModels size)
            {
                var _size = _context.Sizes.SingleOrDefault(br => br.Id == size.Id);
                _size.SizeName = size.SizeName;
                _size.CreatedDate = size.CreatedDate;
                _context.SaveChanges();
            }
        }
    }
}
