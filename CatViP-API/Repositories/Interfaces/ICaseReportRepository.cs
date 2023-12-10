using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface ICaseReportRepository
    {
        ICollection<CatCaseReportImage> GetCaseReportImages(long id);
        ICollection<CatCaseReport> GetOwnCaseReports(long autId);
        Task<bool> StoreCaseReport(CatCaseReport catCaseReport);
    }
}
