using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.ViewModel;
using System.Data.SqlTypes;

namespace MobileShopAPI.Services
{
    public interface ISearchSerivce
    {
        public Task<List<Product>> SearchProduct(SearchViewModel model);
    }

    public class SearchService:ISearchSerivce
    {
        private readonly ApplicationDbContext _context;
        public SearchService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Product>> SearchProduct(SearchViewModel model)
        {
            var productList = await _context.Products.AsNoTracking()
                .Where(p=>p.Name.Contains(model.KeyWord.Trim()))
                .ToListAsync();
            return productList;
        }
    }
}
