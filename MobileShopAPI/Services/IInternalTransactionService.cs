using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MobileShopAPI.Services
{
    public interface IInternalTransactionService
    {
        Task<List<InternalTransaction>> GetAllAsync();

        Task<List<InternalTransaction?>> GetByUserIdAsync(string id);

        Task<List<InternalTransaction>> GetBySubscriptionIdAsync(long id);

        Task<InternalTransactionResponse> AddAsync(InternalTransactionViewModel internalTransaction);
    }

    public class InternalTransactionService : IInternalTransactionService
    {
        private readonly ApplicationDbContext _context;

        public InternalTransactionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<InternalTransactionResponse> AddAsync(InternalTransactionViewModel internalTransaction)
        {
            var SP = _context.SubscriptionPackages.Include(p => p.InternalTransactions).Where(sp => sp.Id == internalTransaction.SpId).FirstOrDefault();

            var CA = _context.CoinActions.Include(p => p.InternalTransactions).Where(sp => sp.Id == internalTransaction.CoinActionId).FirstOrDefault();

            var _it = new InternalTransaction
            {
                Id = StringIdGenerator.GenerateUniqueId(),
                UserId = internalTransaction.UserId,
                CoinActionId = internalTransaction.CoinActionId,
                SpId = internalTransaction.SpId,
                ItAmount = SP.PostAmout,
                ItInfo = CA.Description,
                ItSecureHash = null,
                CreatedDate = null
            };
            _context.Add(_it);
            await _context.SaveChangesAsync();

            return new InternalTransactionResponse
            {
                Message = "New Internal Transaction Added!",
                isSuccess = true
            };
        }

        public async Task<List<InternalTransaction>> GetAllAsync()
        {
            var its = await _context.InternalTransactions.ToListAsync();
            return its;
        }

        public async Task<List<InternalTransaction>> GetBySubscriptionIdAsync(long id)
        {
            var its = await _context.InternalTransactions.Where(sp => sp.SpId == id).ToListAsync();
            return its;
        }

        public async Task<List<InternalTransaction?>> GetByUserIdAsync(string id)
        {
            var its = await _context.InternalTransactions.Where(sp => sp.UserId == id).ToListAsync();
            return its;
        }
    }
}
