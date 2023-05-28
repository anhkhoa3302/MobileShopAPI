using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using System.Drawing.Drawing2D;

namespace MobileShopAPI.Services
{
    public interface ISizeService
    {
        Task<List<Size>> GetAll();

        Task<Size> GetById(long id);

        Task<SizeResponse> Add(SizeViewModel size);

        Task<SizeResponse> Update(long id,SizeViewModel size);

        Task<SizeResponse> Delete(long id);

        public class SizeService : ISizeService
        {
            private readonly ApplicationDbContext _context;

            public SizeService(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<SizeResponse> Add(SizeViewModel size)
            {
                var _size = new Size
                {
                    SizeName = size.SizeName,
                    Description = size.Description
                };
                _context.Add(_size);
                await _context.SaveChangesAsync();

                return new SizeResponse
                {
                    Message = "New Size Added!",
                    isSuccess = true
                };

            }

            public async Task<List<Size>> GetAll()
            {
                var sizes = await _context.Sizes.ToListAsync();
                return sizes;
            }

            public async Task<Size> GetById(long id)
            {
                var size = await _context.Sizes.FindAsync(id);
                if (size != null)
                {
                    return size;
                }
                return null;
            }

            public async Task<SizeResponse> Update(long id,SizeViewModel size)
            {
                var _size = await _context.Sizes.FindAsync(id);
                if (_size == null)
                    return new SizeResponse
                    {
                        Message = "Size not found!",
                        isSuccess = false
                    };
                _size.SizeName = size.SizeName;
                _size.Description = size.Description;
                _size.UpdatedDate = DateTime.Now;
                _context.SaveChanges();

                return new SizeResponse
                {
                    Message = "This Size Updated!",
                    isSuccess = true
                };
            }
            public async Task<SizeResponse> Delete(long id)
            {
                var size = await _context.Sizes.FindAsync(id);

                if (size == null)
                    return new SizeResponse
                    {
                        Message = "Delete Fail",
                        isSuccess = false
                    };

                _context.Remove(size);
                await _context.SaveChangesAsync();

                return new SizeResponse
                {
                    Message = "This Size Deleted",
                    isSuccess = true
                };
            }
        }
    }
}
