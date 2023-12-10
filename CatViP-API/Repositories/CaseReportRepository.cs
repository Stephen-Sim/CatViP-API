using CatViP_API.Data;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatViP_API.Repositories
{
    public class CaseReportRepository : ICaseReportRepository
    {
        private readonly CatViPContext _context;

        public CaseReportRepository(CatViPContext context)
        {
            this._context = context;
        }

        public ICollection<CatCaseReportImage> GetCaseReportImages(long id)
        {
            return _context.CatCaseReportImages.Where(x => x.CatCaseReportId == id).ToList();
        }

        public ICollection<CatCaseReport> GetOwnCaseReports(long autId)
        {
            return _context.CatCaseReports.Include(x => x.Cat).Where(x => x.UserId == autId && x.CatCaseReportStatusId == 1).ToList();
        }

        public async Task<bool> StoreCaseReport(CatCaseReport catCaseReport)
        {
            try
            {
                _context.Add(catCaseReport);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
