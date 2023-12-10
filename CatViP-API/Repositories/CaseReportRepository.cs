using CatViP_API.Data;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;

namespace CatViP_API.Repositories
{
    public class CaseReportRepository : ICaseReportRepository
    {
        private readonly CatViPContext _context;

        public CaseReportRepository(CatViPContext context)
        {
            this._context = context;
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
