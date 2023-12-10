using CatViP_API.DTOs.CaseReportDTOs;

namespace CatViP_API.Services.Interfaces
{
    public interface ICaseReportService
    {
        Task<ResponseResult> CreateCaseReport(long id, CaseReportRequestDTO caseReportRequestDTO);
    }
}
