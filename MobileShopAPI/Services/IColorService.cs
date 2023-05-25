using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using System.Drawing.Drawing2D;

namespace MobileShopAPI.Services
{
    public interface IColorService
    {
        List<ColorViewModels> GetAll();

        ColorViewModels GetById(long id);

        ColorViewModels Add(Color color);

        void Update(ColorViewModels color);

        void Delete(long id);
    }

    public class ColorService : IColorService
    {
        private readonly ApplicationDbContext _context;

        public ColorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public ColorViewModels Add(Color color)
        {
            var _color = new Color
            {
                ColorName = color.ColorName,
                CreatedDate = color.CreatedDate
            };
            _context.Add(_color);
            _context.SaveChanges();

            return new ColorViewModels
            {
                Id = _color.Id,
                ColorName = _color.ColorName,
                CreatedDate = _color.CreatedDate
            };
        }

        public void Delete(long id)
        {
            var color = _context.Colors.SingleOrDefault(br => br.Id == id);
            if (color != null)
            {
                _context.Remove(color);
                _context.SaveChanges();
            }
        }

        public List<ColorViewModels> GetAll()
        {
            var colors = _context.Colors.Select(br => new ColorViewModels
            {
                Id = br.Id,
                ColorName = br.ColorName,
                CreatedDate = br.CreatedDate
            });
            return colors.ToList();
        }

        public ColorViewModels GetById(long id)
        {
            var color = _context.Colors.SingleOrDefault(b => b.Id == id);
            if (color != null)
            {
                return new ColorViewModels
                {
                    Id = color.Id,
                    ColorName = color.ColorName,
                    CreatedDate = color.CreatedDate
                };
            }
            return null;
        }

        public void Update(ColorViewModels color)
        {
            var _color = _context.Colors.SingleOrDefault(br => br.Id == color.Id);
            _color.ColorName = color.ColorName;
            _color.CreatedDate = color.CreatedDate;
            _context.SaveChanges();
        }
    }
}
