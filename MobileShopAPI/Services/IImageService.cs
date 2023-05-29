using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.ViewModel;
using System.Numerics;

namespace MobileShopAPI.Services
{
    public interface IImageService
    {
        Task<List<Image>> GetByProductIdAsync(long productId);

        Task<Image?> GetCoverAsync(long productId);

        Task AddImageAsync(long productId,ImageViewModel image);

        //Task UpdateAsync(ImageViewModel image);

        Task DeleteAsync(long id);
    }

    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;

        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddImageAsync(long productId,ImageViewModel image)
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

        public async Task<List<Image>> GetByProductIdAsync(long productId)
        {
            var images = await _context.Images.Where(s => s.ProductId == productId).ToListAsync();
            return images;
        }

        public async Task<Image?> GetCoverAsync(long productId)
        {
            var image = await _context.Images.Where(s=>s.IsCover == true && s.ProductId == productId).SingleOrDefaultAsync();
            if (image != null)
            {
                return image;
            }
            return null;
        }

        //public async Task UpdateAsync(ImageViewModel image)
        //{
        //    var _image = await _context.Images.FindAsync(image.Id);
        //    if (_image == null)
        //        return;
        //    _image.Url = image.Url;
        //    _image.IsCover = image.IsCover;
        //    _image.UpdatedDate = DateTime.Now;
        //    _context.SaveChanges();
        //}
    }
}
