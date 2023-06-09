using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Controllers;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MobileShopAPI.Services
{
    public interface ISubscriptionPackageService
    {
        Task<List<SubscriptionPackage>> GetAllAsync();

        Task<SubscriptionPackage?> GetByIdAsync(long id);

        Task<SubscriptionPackageResponse> AddAsync(SubscriptionPackageViewModel sp, string usrId);

        Task<SubscriptionPackageResponse> UpdateAsync(long id, SubscriptionPackageViewModel sp);

        Task<SubscriptionPackageResponse> DeleteAsync(long id);
    }

    public class SubscriptionPackageService : ISubscriptionPackageService
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionPackageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SubscriptionPackageResponse> AddAsync(SubscriptionPackageViewModel sp, string usrId)
        {
            var _sp = new SubscriptionPackage
            {
                Name = sp.Name,
                Description = sp.Description,
                Price = sp.Price,
                PostAmout = sp.PostAmout,
                ExpiredIn = sp.ExpiredIn,
                CreatedDate = null
            };
            _context.Add(_sp);
            await _context.SaveChangesAsync();

            // Thêm Active Subscription
            var _as = new ActiveSubscription
            {
                UsedPost = 1,
                ExpiredDate = null,
                ActivatedDate = DateTime.Now,
                SpId = _context.SubscriptionPackages.Max(p => p.Id),
                UserId = usrId
            };
            _context.Add(_as);

            await _context.SaveChangesAsync();


            // Thêm Internal Transaction
            var SubPacID = _context.SubscriptionPackages.Max(p => p.Id);

            var SP = _context.SubscriptionPackages.Include(p => p.InternalTransactions).Where(sp => sp.Id == SubPacID).FirstOrDefault();

            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.Id == 1).FirstOrDefault();

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = usrId,
                CoinActionId = 1,
                SpId = SubPacID,
                ItAmount = SP.PostAmout,
                ItInfo = CA.Description,
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            return new SubscriptionPackageResponse
            {
                Message = "New Subscription Package Added!",
                isSuccess = true
            };
        }

        public async Task<SubscriptionPackageResponse> DeleteAsync(long id)
        {
            var sp = await _context.SubscriptionPackages.SingleOrDefaultAsync(br => br.Id == id);
            if (sp != null)
            {
                _context.Remove(sp);
                await _context.SaveChangesAsync();

                return new SubscriptionPackageResponse
                {
                    Message = "This Subscription Package Deleted",
                    isSuccess = true
                };
            }

            return new SubscriptionPackageResponse
            {
                Message = "DeleteAsync Fail !!!",
                isSuccess = false
            };
        }

        public async Task<List<SubscriptionPackage>> GetAllAsync()
        {
            var sps = await _context.SubscriptionPackages.ToListAsync();
            return sps;
        }

        public async Task<SubscriptionPackage?> GetByIdAsync(long id)
        {
            var sp = await _context.SubscriptionPackages.SingleOrDefaultAsync(b => b.Id == id);
            if (sp != null)
            {
                return sp;
            }
            return null;
        }

        public async Task<SubscriptionPackageResponse> UpdateAsync(long id, SubscriptionPackageViewModel sp)
        {
            var _sp = await _context.SubscriptionPackages.FindAsync(id);

            if (_sp == null)
                return new SubscriptionPackageResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };

            _sp.Name = sp.Name;
            _sp.Description = sp.Description;
            _sp.Price = sp.Price;
            _sp.PostAmout = sp.PostAmout;
            _sp.ExpiredIn = sp.ExpiredIn;
            _sp.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new SubscriptionPackageResponse
            {
                Message = "This Subscription Package Updated!",
                isSuccess = true
            };
        }
    }
}
