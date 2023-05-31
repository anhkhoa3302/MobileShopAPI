using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.ViewModel;
using System.Numerics;

namespace MobileShopAPI.Services
{
    public interface IImageService
    {
        Task AddAsync(long productId,ImageViewModel image);

        Task UpdateAsync(ImageViewModel image);

        Task DeleteAsync(long id);
    }

    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;

        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(long productId,ImageViewModel image)
        {
            var _image = new Image
            {
                Url = image.Url,
                IsCover = image.IsCover,
                ProductId = productId
            };
            _context.Images.Add(_image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Remove(image);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(ImageViewModel image)
        {
            var _image = await _context.Images.FindAsync(image.Id);
            if (_image == null)
                return;
            _image.IsCover = image.IsCover;
            _image.UpdatedDate = DateTime.Now;
            _context.Images.Update(_image);
            await _context.SaveChangesAsync();
        }
    }
}
