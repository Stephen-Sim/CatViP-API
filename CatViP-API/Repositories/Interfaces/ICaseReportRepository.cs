using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface ICaseReportRepository
    {
        Task<bool> StoreCaseReport(CatCaseReport catCaseReport);
    }
}
