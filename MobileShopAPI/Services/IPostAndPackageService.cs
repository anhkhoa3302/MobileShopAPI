using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IPostAndPackageService
    {
        Task<ProductResponse> CreateProductAsync(Product5ViewModel model, string UsrId);

        Task<ActiveSubscriptionResponse> AS_BuyPackageAsync(AddActiveSubscriptionViewModel model, string UsrId);

        Task<ActiveSubscriptionResponse> AS_PostAsync(long SubPacId, string UsrId);

        Task<ActiveSubscriptionResponse> AS_PushUpAsync(string UsrId, long SubPacId);

        Task<InternalTransactionResponse> IT_BuyPackageAsync(string UsrId, long? SubPacId);

        Task<InternalTransactionResponse> IT_PostAsync(string UsrId, long? SubPacId);

        Task<InternalTransactionResponse> IT_PushUpAsync(string UsrId, long SubPacId, long ProductId);

        Task<ProductResponse> SetPriorities(long id);

        Task<List<Product>> GetAll();

        Task<ProductResponse> HideProduct(long id);

        Task<ProductResponse> AutoHideProduct(string UsrId);


    }

    public class PostAndPackageService : IPostAndPackageService
    {
        private readonly ApplicationDbContext _context;

        private readonly IImageService _imageService;

        public PostAndPackageService(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

         public async Task<ProductResponse> CreateProductAsync(Product5ViewModel model, string UsrId)
        {
            if (model == null)
                return new ProductResponse
                {
                    Message = "Model is null",
                    isSuccess = false
                };
            if (model.Images == null)
                return new ProductResponse
                {
                    Message = "Images list is null",
                    isSuccess = false
                };
            if (model.Images.Count < 2)
            {
                return new ProductResponse
                {
                    Message = "Product need at least 2 image",
                    isSuccess = false
                };
            }
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Stock = model.Stock,
                Price = model.Price,
                Status = model.Status,
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                UserId = UsrId,
                SizeId = model.SizeId,
                Priorities = 2,
                ColorId = model.ColorId,
                ExpiredDate = DateTime.Now.AddDays(60)
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            bool hasCover = false;
            if (model.Images != null)
                foreach (var item in model.Images)
                {
                    var image = new ImageViewModel();
                    if (!hasCover)
                    {

                        image.IsCover = true;
                        hasCover = true;
                    }
                    else
                    {
                        image.IsCover = false;
                    }
                    image.Url = item.Url;
                    await _imageService.AddAsync(product.Id, image);
                }

            return new ProductResponse
            {
                Message = "Product has been created successfully",
                isSuccess = true
            };
        }

        public async Task<ActiveSubscriptionResponse> AS_BuyPackageAsync(AddActiveSubscriptionViewModel model, string UsrId)
        {
            var _model = new ActiveSubscription
            {
                UsedPost = 0,
                ExpiredDate = DateTime.Now.AddDays(60),
                ActivatedDate = DateTime.Now,
                UserId = UsrId,
                SpId = model.SpId,
            };
            _context.Add(_model);
            await _context.SaveChangesAsync();

            return new ActiveSubscriptionResponse
            {
                Message = "New Active Subscription Added!",
                isSuccess = true
            };
        }

        public async Task<ActiveSubscriptionResponse> AS_PostAsync(long SubPacId, string UsrId)
        {
            var sp = await _context.SubscriptionPackages.FirstOrDefaultAsync(p => p.Id == SubPacId);
            var _model = await _context.ActiveSubscriptions.Where(p => (p.SpId == SubPacId && p.UserId == UsrId)).FirstOrDefaultAsync();

            if(sp != null && _model != null)
            {
                // Free Subcription Package
                if(SubPacId == 1) 
                {
                    if (_model.UsedPost == sp.PostAmout)
                    {
                        _model.UsedPost = 0;
                        _model.ExpiredDate = _model.ExpiredDate;
                        _model.ActivatedDate = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _model.UsedPost = _model.UsedPost + 1;
                        _model.ExpiredDate = _model.ExpiredDate;
                        _model.ActivatedDate = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }    
                }
                else // Paid Subcription Package
                {
                    if (_model.UsedPost == sp.PostAmout)
                    {
                        _context.Remove(_model);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _model.UsedPost = _model.UsedPost + 1;
                        _model.ExpiredDate = _model.ExpiredDate;
                        _model.ActivatedDate = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                }          
            }
            else
            {
                return new ActiveSubscriptionResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };
            }

            //if (_model == null)
            //{
            //    return new ActiveSubscriptionResponse
            //    {
            //        Message = "Bad request",
            //        isSuccess = false
            //    };
            //}
            //else
            //{

            //    _model.UsedPost = _model.UsedPost + 1;
            //    _model.ExpiredDate = _model.ExpiredDate;
            //    _model.ActivatedDate = DateTime.Now;
            //    await _context.SaveChangesAsync();
            //}

            return new ActiveSubscriptionResponse
            {
                Message = "this active subscription updated!",
                isSuccess = true
            };
        }

        public async Task<ActiveSubscriptionResponse> AS_PushUpAsync(string UsrId, long SubPacId)
        {
            var _model = await _context.ActiveSubscriptions.Where(p => (p.SpId == SubPacId && p.UserId == UsrId)).FirstAsync();


            if (_model == null)
            {
                return new ActiveSubscriptionResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };
            }
            else
            {

                _model.UsedPost = _model.UsedPost + 2;
                _model.ExpiredDate = _model.ExpiredDate;
                _model.ActivatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return new ActiveSubscriptionResponse
            {
                Message = "This Active Subscription Updated!",
                isSuccess = true
            };
        }

        // Mua gói tin đăng
        public async Task<InternalTransactionResponse> IT_BuyPackageAsync(string UsrId, long? SubPacId)
        {
            var SP = _context.SubscriptionPackages.Include(p => p.InternalTransactions).Where(sp => sp.Id == SubPacId).FirstOrDefault();

            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.Id == 2).FirstOrDefault();

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = UsrId,
                CoinActionId = 2,
                SpId = SubPacId,
                ItAmount = SP.PostAmout,
                ItInfo = CA.Description,
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            return new InternalTransactionResponse
            {
                Message = "New Internal Transaction (Buy Package) Added!",
                isSuccess = true
            };
        }

        // Đăng tin
        public async Task<InternalTransactionResponse> IT_PostAsync(string UsrId, long? SubPacId)
        {
            var SP = _context.SubscriptionPackages.Include(p => p.InternalTransactions).Where(sp => sp.Id == SubPacId).FirstOrDefault();

            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.Id == 3).FirstOrDefault();

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = UsrId,
                CoinActionId = 3,
                SpId = SubPacId,
                ItAmount = SP.PostAmout,
                ItInfo = CA.Description,
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            return new InternalTransactionResponse
            {
                Message = "New Internal Transaction (Post) Added!",
                isSuccess = true
            };
        }

        // Đẩy tin
        public async Task<InternalTransactionResponse> IT_PushUpAsync(string UsrId, long SubPacId, long ProductId)
        {
            var SP = _context.SubscriptionPackages.Include(p => p.InternalTransactions).Where(sp => sp.Id == SubPacId).FirstOrDefault();

            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.Id == 1).FirstOrDefault();

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = UsrId,
                CoinActionId = 1,
                SpId = SubPacId,
                ItAmount = SP.PostAmout,
                ItInfo = ProductId.ToString(),
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            return new InternalTransactionResponse
            {
                Message = "New Internal Transaction (Push Up) Added!",
                isSuccess = true
            };
        }

        // Đẩy tin
        public async Task<ProductResponse> SetPriorities(long id)
        {
            var pro = await _context.Products.FindAsync(id);

            pro.Priorities = 1;
            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Message = "Product has been pushed up successfully",
                isSuccess = true
            };
        }

        // Lấy danh sách sản phẩm ưu tiên
        public async Task<List<Product>> GetAll()
        {
            var products = await _context.Products.Where(t => t.isHidden == false).OrderBy(p => p.Priorities).ToListAsync();
            return products;
        }


        // Ẩn tin
        public async Task<ProductResponse> HideProduct(long id)
        {
            var pro = await _context.Products.FindAsync(id);

            pro.isHidden = true;
            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Message = "Product has been hidden successfully",
                isSuccess = true
            };
        }

        // Tự động ẩn tin, ngưng đẩy tin
        public async Task<ProductResponse> AutoHideProduct(string UsrId)
        {
            var pro = await _context.Products.ToListAsync();

            
            foreach (var item in pro)
            {
                if(item.ExpiredDate <= DateTime.Now)
                {
                    item.isHidden = true;
                    await _context.SaveChangesAsync();
                }

                if ((item.isHidden == true) && (item.ExpiredDate <= DateTime.Now.AddDays(-30)))
                {
                    item.Status = 3;
                    await _context.SaveChangesAsync();
                }

                if(item.Priorities == 1)
                {
                    var it_info = await _context.InternalTransactions.Where(p => p.UserId == UsrId && p.ItInfo == item.Id.ToString()).FirstOrDefaultAsync();
                    if ((it_info != null) && it_info.CreatedDate <= DateTime.Now.AddDays(-7))
                    {
                        item.Priorities = 2;
                        await _context.SaveChangesAsync();
                    }    
                }    
            }    

            return new ProductResponse
            {
                Message = "Auto Hide Completed",
                isSuccess = true
            };
        }
    }
}
