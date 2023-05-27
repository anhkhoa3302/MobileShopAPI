using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using System.Drawing.Drawing2D;

namespace MobileShopAPI.Services
{
    public interface ISizeService
    {
        List<Size> GetAll();

        Size GetById(long id);

        Task<SizeResponse> Add(Size size);

        Task<SizeResponse> Update(Size size);

        Task<SizeResponse> Delete(long id);

        public class SizeService : ISizeService
        {
            private readonly ApplicationDbContext _context;

            public SizeService(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<SizeResponse> Add(Size size)
            {
                var _size = new Size
                {
                    SizeName = size.SizeName,
                    CreatedDate = null
                };
                _context.Add(_size);
                _context.SaveChanges();

                return new SizeResponse
                {
                    Message = "New Size Added!",
                    isSuccess = true
                };

                //return new Size
                //{
                //    Id = _size.Id,
                //    SizeName = _size.SizeName,
                //    CreatedDate = _size.CreatedDate
                //};
            }

            public async Task<SizeResponse> Delete(long id)
            {
                var size = _context.Sizes.SingleOrDefault(br => br.Id == id);
                if (size != null)
                {
                    _context.Remove(size);
                    _context.SaveChanges();

                    return new SizeResponse
                    {
                        Message = "This Size Deleted",
                        isSuccess = true
                    };
                }

                return new SizeResponse
                {
                    Message = "Delete Fail",
                    isSuccess = false
                };
            }

            public List<Size> GetAll()
            {
                var sizes = _context.Sizes.Select(br => new Size
                {
                    Id = br.Id,
                    SizeName = br.SizeName,
                    CreatedDate = br.CreatedDate
                });
                return sizes.ToList();
            }

            public Size GetById(long id)
            {
                var size = _context.Sizes.SingleOrDefault(b => b.Id == id);
                if (size != null)
                {
                    return new Size
                    {
                        Id = size.Id,
                        SizeName = size.SizeName,
                        CreatedDate = size.CreatedDate
                    };
                }
                return null;
            }

            public async Task<SizeResponse> Update(Size size)
            {
                var _size = _context.Sizes.SingleOrDefault(br => br.Id == size.Id);
                _size.SizeName = size.SizeName;
                _size.CreatedDate = size.CreatedDate;
                _context.SaveChanges();

                return new SizeResponse
                {
                    Message = "This Size Updated!",
                    isSuccess = true
                };
            }
        }
    }
}
