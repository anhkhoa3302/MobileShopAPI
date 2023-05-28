using MobileShopAPI.Data;
using MobileShopAPI.Models;
using System.Numerics;

namespace MobileShopAPI.Services
{
    public interface IImageService
    {
        List<Image> GetAll();

        Image GetById(long id);

        Image Add(Image image);

        void Update(Image image);

        void Delete(long id);
    }

    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;

        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Image Add(Image image)
        {
            var _image = new Image
            {
                Id = image.Id,
                Url = image.Url,
                IsCover = image.IsCover,
                ProductId = image.ProductId,
                CreatedDate = image.CreatedDate
            };
            _context.Add(_image);
            _context.SaveChanges();

            return _image;
            //return new Image
            //{
            //    Id = _image.Id,
            //    Url = _image.Url,
            //    IsCover = _image.IsCover,
            //    CreatedDate = _image.CreatedDate
            //};
        }

        public void Delete(long id)
        {
            var image = _context.Images.SingleOrDefault(img => img.Id == id);
            if (image != null)
            {
                _context.Remove(image);
                _context.SaveChanges();
            }
        }

        public List<Image> GetAll()
        {
            var images = _context.Images.Select(img => new Image
            {
                Id = img.Id,
                Url = img.Url,
                IsCover = img.IsCover,
                ProductId = img.ProductId,
                CreatedDate = img.CreatedDate
            });
            return images.ToList();
        }

        public Image? GetById(long id)
        {
            var image = _context.Images.SingleOrDefault(img => img.Id == id);
            if (image != null)
            {
                return new Image
                {
                    Id = image.Id,
                    Url = image.Url,
                    IsCover = image.IsCover,
                    ProductId = image.ProductId,
                    CreatedDate = image.CreatedDate
                };
            }
            return null;
        }

        public void Update(Image image)
        {
            var _image = _context.Images.SingleOrDefault(img => img.Id == image.Id);
            _image.Id = image.Id;
            _image.Url = image.Url;
            _image.IsCover = image.IsCover;
            _image.ProductId = image.ProductId;
            _image.CreatedDate = image.CreatedDate;
            _context.SaveChanges();
        }
    }
}
