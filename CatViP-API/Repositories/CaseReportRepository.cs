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

        public bool CheckIsReportExist(long authId, long id)
        {
            return _context.CatCaseReports.Any(x => x.UserId == authId && x.Id == id);
        }

        public ICollection<CatCaseReportImage> GetCaseReportImages(long id)
        {
            return _context.CatCaseReportImages.Where(x => x.CatCaseReportId == id).ToList();
        }

        public ICollection<CatCaseReport> GetOwnCaseReports(long autId)
        {
            return _context.CatCaseReports.Include(x => x.Cat).Where(x => x.UserId == autId && x.CatCaseReportStatusId == 1).ToList();
        }

        public async Task<bool> RevokeCaseReport(long id)
        {
            try
            {
                var caseReport = _context.CatCaseReports.FirstOrDefault(x => x.Id == id);
                caseReport!.CatCaseReportStatusId = 3;
                _context.Update(caseReport);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SettleCaseReport(long id)
        {
            try
            {
                var caseReport = _context.CatCaseReports.FirstOrDefault(x => x.Id == id);
                caseReport!.CatCaseReportStatusId = 2;
                _context.Update(caseReport);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
