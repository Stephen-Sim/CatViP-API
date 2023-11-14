using CatViP_API.DTOs.ExpertDTOs;

namespace CatViP_API.Services.Interfaces
{
    public interface IExpertService
    {
        Task<ResponseResult> ApplyAsExpert(long userId, ExpertApplicationRequestDTO expertApplicationRequestDTO);
        ICollection<ExpertApplicationDTO> GetApplications(long userId);
        ICollection<ExpertApplicationDTO> GetPendingApplications();
        Task<ResponseResult> UpdateApplicationStatus(ExpertApplicationActionRequestDTO expertApplicationActionRequestDTO);
        ResponseResult CheckIfPendingApplicationExist(long applicationId);
    }
}
