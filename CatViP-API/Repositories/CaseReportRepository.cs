using CatViP_API.Data;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

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
            return _context.CatCaseReports.Any(x => x.UserId == authId && x.Id == id && x.CatCaseReportStatusId == 1);
        }

        public ICollection<CatCaseReportImage> GetCaseReportImages(long id)
        {
            return _context.CatCaseReportImages.Where(x => x.CatCaseReportId == id).ToList();
        }

        public ICollection<CatCaseReport> GetNotAuthCaseReports(long authId)
        {
            return _context.CatCaseReports.Include(x => x.User).Where(x => x.UserId != authId && x.CatCaseReportStatusId == 1).OrderByDescending(x => x.DateTime).ToList();
        }

        public ICollection<CatCaseReport> GetCaseReportsMoreThan7Days()
        {
            return _context.CatCaseReports.Where(x => x.CatCaseReportStatusId == 1 && x.DateTime < DateTime.Now.AddDays(-7)).OrderByDescending(x => x.DateTime).ToList();
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

        public CatCaseReport? GetNotAuthCaseReport(long Id, long userId)
        {
            return _context.CatCaseReports.Include(x => x.User).FirstOrDefault(x => x.Id == Id && x.UserId != userId && x.CatCaseReportStatusId == 1);
        }

        public CatCaseReport? GetOwnCaseReport(long Id, long userId)
        {
            return _context.CatCaseReports.Include(x => x.User).FirstOrDefault(x => x.Id == Id && x.UserId == userId && x.CatCaseReportStatusId == 1);
        }
    }
}
