using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IStatisticsService
    {
        Task<List<GetPostsByCateViewModel>> GetPostsByCategoryInDay();

        Task<List<GetPostsByCateViewModel>> GetPostsByCategoryInWeek();

        Task<List<GetPostsByCateViewModel>> GetPostsByCategoryInMonth();

        Task<List<GetPostsByCateViewModel>> GetPostsByCategoryInStages(PeriodViewModel model);

        Task<List<GetPostsByUserViewModel>> GetPostsByUserInDay();

        Task<List<GetPostsByUserViewModel>> GetPostsByUserInWeek();

        Task<List<GetPostsByUserViewModel>> GetPostsByUserInMonth();

        Task<List<GetPostsByUserViewModel>> GetPostsByUserInStages(PeriodViewModel model);

        Task<List<GetUserByPostedViewModel>> GetUserByPostInStages(PeriodViewModel model);

        Task<List<GetUserByBuyPackageViewModel>> GetUserByPackagePurchasesInStages(PeriodViewModel model);

    }

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetPostsByCateViewModel>> GetPostsByCategoryInDay()
        {
            var pro = await _context.Products.Include(o => o.Category).Where(p => p.CreatedDate >= DateTime.Today && p.CreatedDate < DateTime.Now.AddDays(1).Date).ToListAsync();

            //return pro;

            //int[] arr1 = new int[100];
            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].CategoryId == pro[j].CategoryId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var cateViewList = new List<GetPostsByCateViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetPostsByCateViewModel
                    {
                        Category = pro[i].Category.Name,
                        Posted = fr1[i]
                    };
                    cateViewList.Add(temp);
                }

            }

            // Begin Updated
            cateViewList = cateViewList.OrderByDescending(p => p.Posted).ToList();
            // End Updated

            return cateViewList;
        }

        public async Task<List<GetPostsByCateViewModel>> GetPostsByCategoryInWeek()
        {
            var pro = await _context.Products.Include(o => o.Category).Where(p => p.CreatedDate < DateTime.Now.AddDays(1).Date && p.CreatedDate >= DateTime.Now.AddDays(-7).Date).ToListAsync();

            //return pro;

            //int[] arr1 = new int[100];
            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].CategoryId == pro[j].CategoryId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var cateViewList = new List<GetPostsByCateViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetPostsByCateViewModel
                    {
                        Category = pro[i].Category.Name,
                        Posted = fr1[i]
                    };
                    cateViewList.Add(temp);
                }

            }

            // Begin Updated
            cateViewList = cateViewList.OrderByDescending(p => p.Posted).ToList();
            // End Updated

            return cateViewList;
        }

        public async Task<List<GetPostsByCateViewModel>> GetPostsByCategoryInMonth()
        {
            var pro = await _context.Products.Include(o => o.Category).Where(p => p.CreatedDate < DateTime.Now.AddDays(1).Date && p.CreatedDate >= DateTime.Now.AddDays(-30).Date).ToListAsync();

            //return pro;

            //int[] arr1 = new int[100];
            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].CategoryId == pro[j].CategoryId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var cateViewList = new List<GetPostsByCateViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetPostsByCateViewModel
                    {
                        Category = pro[i].Category.Name,
                        Posted = fr1[i]
                    };
                    cateViewList.Add(temp);
                }

            }

            // Begin Updated
            cateViewList = cateViewList.OrderByDescending(p => p.Posted).ToList();
            // End Updated

            return cateViewList;
        }

        public async Task<List<GetPostsByCateViewModel>> GetPostsByCategoryInStages(PeriodViewModel model)
        {
            var pro = await _context.Products.Include( o => o.Category).Where(p => p.CreatedDate >= model.starDate && p.CreatedDate <= model.endDate).ToListAsync();

            //return pro;

            //int[] arr1 = new int[100];
            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].CategoryId == pro[j].CategoryId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var cateViewList = new List<GetPostsByCateViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetPostsByCateViewModel
                    {
                        Category = pro[i].Category.Name,
                        Posted = fr1[i]
                    };
                    cateViewList.Add(temp);
                }

            }

            // Begin Updated
            cateViewList = cateViewList.OrderByDescending(p => p.Posted).ToList();
            // End Updated

            return cateViewList;
        }

        public async Task<List<GetPostsByUserViewModel>> GetPostsByUserInDay()
        {
            var pro = await _context.Products.Include(u => u.User).Where(p => p.CreatedDate >= DateTime.Today && p.CreatedDate < DateTime.Now.AddDays(1).Date).ToListAsync();

            //int[] arr1 = new int[100];
            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].UserId == pro[j].UserId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var userViewList = new List<GetPostsByUserViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetPostsByUserViewModel
                    {
                        User = pro[i].User.NormalizedUserName,
                        Posted = fr1[i]
                    };
                    userViewList.Add(temp);
                }

            }

            return userViewList;
        }

        public async Task<List<GetPostsByUserViewModel>> GetPostsByUserInWeek()
        {
            var pro = await _context.Products.Include(u => u.User).Where(p => p.CreatedDate < DateTime.Now.AddDays(1).Date && p.CreatedDate >= DateTime.Now.AddDays(-7).Date).ToListAsync();

            //int[] arr1 = new int[100];
            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].UserId == pro[j].UserId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var userViewList = new List<GetPostsByUserViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetPostsByUserViewModel
                    {
                        User = pro[i].User.NormalizedUserName,
                        Posted = fr1[i]
                    };
                    userViewList.Add(temp);
                }

            }

            return userViewList;
        }

        public async Task<List<GetPostsByUserViewModel>> GetPostsByUserInMonth()
        {
            var pro = await _context.Products.Include(u => u.User).Where(p => p.CreatedDate < DateTime.Now.AddDays(1).Date && p.CreatedDate >= DateTime.Now.AddDays(-30).Date).ToListAsync();

            //int[] arr1 = new int[100];
            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].UserId == pro[j].UserId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var userViewList = new List<GetPostsByUserViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetPostsByUserViewModel
                    {
                        User = pro[i].User.NormalizedUserName,
                        Posted = fr1[i]
                    };
                    userViewList.Add(temp);
                }

            }

            return userViewList;
        }

        public async Task<List<GetPostsByUserViewModel>> GetPostsByUserInStages(PeriodViewModel model)
        {
            var pro = await _context.Products.Include(u => u.User).Where(p => p.CreatedDate >= model.starDate && p.CreatedDate <= model.endDate).ToListAsync();

            //int[] arr1 = new int[100];
            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].UserId == pro[j].UserId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var userViewList = new List<GetPostsByUserViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetPostsByUserViewModel
                    {
                        User = pro[i].User.NormalizedUserName,
                        Posted = fr1[i]
                    };
                    userViewList.Add(temp);
                }

            }

            return userViewList;
        }

        public async Task<List<GetUserByPostedViewModel>> GetUserByPostInStages(PeriodViewModel model)
        {
            var pro = await _context.Products.Include(u => u.User).Where(p => p.CreatedDate >= model.starDate && p.CreatedDate <= model.endDate).OrderByDescending(o => o.UserId).ToListAsync();

            //int[] arr1 = new int[100];
            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].UserId == pro[j].UserId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var userViewList = new List<GetUserByPostedViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetUserByPostedViewModel
                    {
                        User = pro[i].User.NormalizedUserName,
                        Posted = fr1[i]
                    };
                    userViewList.Add(temp);
                }

            }

            // Begin Updated
            userViewList = userViewList.OrderByDescending(p => p.Posted).ToList();
            // End Updated

            return userViewList;
        }

        public async Task<List<GetUserByBuyPackageViewModel>> GetUserByPackagePurchasesInStages(PeriodViewModel model)
        {
            var pro = await _context.InternalTransactions.Include(u => u.User).Where(p => p.ItInfo == "Mua gói tin" && p.CreatedDate >= model.starDate && p.CreatedDate <= model.endDate).OrderByDescending(o => o.UserId).ToListAsync();

            int[] fr1 = new int[100];
            int n, i, j, bien_dem;

            for (i = 0; i < pro.Count; i++)
            {
                fr1[i] = -1;
            }

            for (i = 0; i < pro.Count; i++)
            {
                bien_dem = 1;
                for (j = i + 1; j < pro.Count; j++)
                {
                    if (pro[i].UserId == pro[j].UserId)
                    {
                        bien_dem++;
                        fr1[j] = 0;
                    }
                }

                if (fr1[i] != 0)
                {
                    fr1[i] = bien_dem;
                }
            }

            var userViewList = new List<GetUserByBuyPackageViewModel>();


            for (i = 0; i < pro.Count; i++)
            {
                if (fr1[i] != 0)
                {

                    var temp = new GetUserByBuyPackageViewModel
                    {
                        User = pro[i].User.NormalizedUserName,
                        Purchases = fr1[i]
                    };
                    userViewList.Add(temp);
                }

            }

            // Begin Updated
            userViewList = userViewList.OrderByDescending(p => p.Purchases).ToList();
            // End Updated

            return userViewList;
        }
    }
}
