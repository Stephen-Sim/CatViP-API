﻿using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface ICaseReportRepository
    {
        bool CheckIsReportExist(long authId, long id);
        ICollection<CatCaseReportImage> GetCaseReportImages(long id);
        ICollection<CatCaseReport> GetCaseReportsMoreThan7Days();
        ICollection<CatCaseReport> GetOwnCaseReports(long autId);
        CatCaseReport? GetOwnCaseReport(long Id, long userId);
        ICollection<CatCaseReport> GetNotAuthCaseReports(long authId);
        CatCaseReport? GetNotAuthCaseReport(long Id, long userId);
        Task<bool> RevokeCaseReport(long id);
        Task<bool> SettleCaseReport(long id);
        Task<bool> StoreCaseReport(CatCaseReport catCaseReport);
    }
}
