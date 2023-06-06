using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Services
{
    public interface IReportService
    {
        Task<List<Report>> GetAllAsync();

        Task<Report?> GetByIdAsync(long id);

        Task<ReportResponse> AddAsync(ReportViewModel report);

        Task<ReportResponse> UpdateAsync(long id, ReportViewModel report);

        Task<ReportResponse> DeleteAsync(long id);
    }

    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ReportResponse> AddAsync(ReportViewModel report)
        {
            var _report = new Report
            {
                Subject = report.Subject,
                Body = report.Body,
                ReportedUserId = report.ReportedUserId,
                ReportedProductId = report.ReportedProductId,
                UserId = report.UserId,
                ReportCategoryId = report.ReportCategoryId,
                CreatedDate = null
            };
            _context.Add(_report);
            await _context.SaveChangesAsync();

            return new ReportResponse
            {
                Message = "New report Added!",
                isSuccess = true
            };

        }

        public async Task<ReportResponse> DeleteAsync(long id)
        {
            var report = await _context.Reports.SingleOrDefaultAsync(report => report.Id == id);
            if (report != null)
            {
                _context.Remove(report);
                await _context.SaveChangesAsync();

                return new ReportResponse
                {
                    Message = "This report Deleted",
                    isSuccess = true
                };
            }

            return new ReportResponse
            {
                Message = "DeleteAsync Fail !!!",
                isSuccess = false
            };
        }

        public async Task<List<Report>> GetAllAsync()
        {
            var reports = await _context.Reports.ToListAsync();
            return reports;
        }

        public async Task<Report?> GetByIdAsync(long id)
        {
            var report = await _context.Reports.SingleOrDefaultAsync(report => report.Id == id);
            if (report != null)
            {
                return report;
            }
            return null;
        }

        public async Task<ReportResponse> UpdateAsync(long id, ReportViewModel report)
        {
            var _report = await _context.Reports.FindAsync(id);

            if (_report == null)
                return new ReportResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };

            _report.Subject = report.Subject;
            _report.Body = report.Body;
            _report.ReportedProductId = report.ReportedProductId;
            _report.ReportCategoryId = report.ReportCategoryId;
            await _context.SaveChangesAsync();

            return new ReportResponse
            {
                Message = "This report Updated!",
                isSuccess = true
            };
        }
    }
}
