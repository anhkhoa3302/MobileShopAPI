using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace MobileShopAPI.Services
{
    public interface IEvidenceService
    {
        Task AddAsync(long reportId, EvidenceViewModel model);
    }

    public class EvidenceService : IEvidenceService
    {
        private readonly ApplicationDbContext _context;

        public EvidenceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(long reportId, EvidenceViewModel model)
        {
            var ev = new Evidence
            {
                ImageUrl = model.ImageUrl,
                ReportId = reportId
            };
            _context.Evidences.Add(ev);
            await _context.SaveChangesAsync();
        }
    }
}
