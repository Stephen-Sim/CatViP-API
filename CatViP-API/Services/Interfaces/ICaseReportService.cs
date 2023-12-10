using CatViP_API.DTOs.CaseReportDTOs;

namespace CatViP_API.Services.Interfaces
{
    public interface ICaseReportService
    {
        ResponseResult CheckIsReportExist(long authId, long id);
        Task<ResponseResult> CreateCaseReport(long id, CaseReportRequestDTO caseReportRequestDTO);
        ICollection<OwnCaseReportDTO> GetOwnCaseReports(long id);
        Task<ResponseResult> RevokeCaseReport(long id);
        Task RevokeCaseReportsMoreThan7Days();
        Task<ResponseResult> SettleCaseReport(long id);
    }
}
