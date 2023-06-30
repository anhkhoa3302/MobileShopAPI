using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.ViewModel;



namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("monthly-registers")]
        public async Task<IActionResult> GetMonthlyNewRegisters(int year)
        {
            var data = await _context.Users.Where(x => x.CreatedDate.Value.Date.Year == year)
               .GroupBy(x => x.CreatedDate.Value.Date.Month)
               .Select(g => new MonthlyNewUsersViewModel()
               {
                   Month = g.Key,
                   NumberOfNewUser = g.Count()
               })
               .ToListAsync();

            return Ok(data);
        }
        [HttpGet("FromTo-registers")]
        public async Task<IActionResult> GetNewRegisters(string fromDate, string toDate)
        {
           
            var query = from u in _context.Users
                        select new
                        {
                            CreateDate = u.CreatedDate,
                            UserId = u.Id,
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate < endDate);
            }
            var result = await query.GroupBy(x => x.CreateDate)
                                .Select(g => new WeeklyNewUserViewModel()
                                {
                                    NumberOfNewUser = g.Count()
                                })
                                .ToListAsync();
            return Ok(result);
        }


        // product

        [HttpGet("monthly-product")]
        public async Task<IActionResult> GetMonthlyNewProducts(int year)
        {
            var data = await _context.Products.Where(x => x.CreatedDate.Value.Date.Year == year)
               .GroupBy(x => x.CreatedDate.Value.Date.Month)
               .Select(g => new MonthlyNewProductViewModel() 
               {
                   Month = g.Key,
                   NumberOfNewProduct = g.Count()
               })
               .ToListAsync();

            return Ok(data);
        }
        [HttpGet("FromTo-Products")]
        public async Task<IActionResult> GetNewProducts(string fromDate, string toDate)
        {

            var query = from u in _context.Products
                        select new
                        {
                            CreateDate = u.CreatedDate,
                            ProductId = u.Id,
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate < endDate);
            }
            var result = await query.GroupBy(x => x.CreateDate)
                                .Select(g => new FromToNewProductViewModel() 
                                {
                                    NumberOfNewProduct = g.Count()
                                })
                                .ToListAsync();
            return Ok(result);
        }


        //report

        [HttpGet("monthly-reports")]
        public async Task<IActionResult> GetMonthlyNewReports(int year)
        {
            var data = await _context.Reports.Where(x => x.CreatedDate.Value.Date.Year == year)
               .GroupBy(x => x.CreatedDate.Value.Date.Month)
               .Select(g => new MonthlyNewReportViewModel() 
               {
                   Month = g.Key,
                   NumberOfNewReport = g.Count()
               })
               .ToListAsync();

            return Ok(data);
        }
        [HttpGet("FromTo-reports")]
        public async Task<IActionResult> GetNewReports(string fromDate, string toDate)
        {

            var query = from u in _context.Reports
                        select new
                        {
                            CreateDate = u.CreatedDate,
                            ReportId = u.Id,
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate < endDate);
            }
            var result = await query.GroupBy(x => x.CreateDate)
                                .Select(g => new FromToNewReportViewModel() 
                                {
                                    NumberOfNewReport = g.Count()
                                })
                                .ToListAsync();
            return Ok(result);
        }

    }
}
