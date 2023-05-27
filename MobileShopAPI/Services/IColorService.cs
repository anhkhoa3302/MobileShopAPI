using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using System.Drawing.Drawing2D;

namespace MobileShopAPI.Services
{
    public interface IColorService
    {
        List<Color> GetAll();

        Color GetById(long id);

        Task<ColorResponse> Add(Color color);

        Task<ColorResponse> Update(Color color);

        Task<ColorResponse> Delete(long id);
    }

    public class ColorService : IColorService
    {
        private readonly ApplicationDbContext _context;

        public ColorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ColorResponse> Add(Color color)
        {
            var _color = new Color
            {
                ColorName = color.ColorName,
                CreatedDate = color.CreatedDate
            };
            _context.Add(_color);
            _context.SaveChanges();


            return new ColorResponse
            {
                Message = "New Color Added!",
                isSuccess = true
            };
            //return new Color
            //{
            //    Id = _color.Id,
            //    ColorName = _color.ColorName,
            //    CreatedDate = _color.CreatedDate
            //};
        }

        public async Task<ColorResponse> Delete(long id)
        {
            var color = _context.Colors.SingleOrDefault(br => br.Id == id);
            if (color != null)
            {
                _context.Remove(color);
                _context.SaveChanges();

                return new ColorResponse
                {
                    Message = "This Color Deleted",
                    isSuccess = true
                };
            }

            return new ColorResponse
            {
                Message = "Delete Fail",
                isSuccess = false
            };
        }

        public List<Color> GetAll()
        {
            var colors = _context.Colors.Select(br => new Color
            {
                Id = br.Id,
                ColorName = br.ColorName,
                CreatedDate = br.CreatedDate
            });
            return colors.ToList();
        }

        public Color GetById(long id)
        {
            var color = _context.Colors.SingleOrDefault(b => b.Id == id);
            if (color != null)
            {
                return new Color
                {
                    Id = color.Id,
                    ColorName = color.ColorName,
                    CreatedDate = color.CreatedDate
                };
            }
            return null;
        }

        public async Task<ColorResponse> Update(Color color)
        {
            var _color = _context.Colors.SingleOrDefault(br => br.Id == color.Id);
            _color.ColorName = color.ColorName;
            _color.CreatedDate = color.CreatedDate;
            _context.SaveChanges();

            return new ColorResponse
            {
                Message = "This Color Updated!",
                isSuccess = true
            };
        }
    }
}
