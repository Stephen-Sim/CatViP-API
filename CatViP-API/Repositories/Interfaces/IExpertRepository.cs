using CatViP_API.DTOs.ExpertDTOs;
using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface IExpertRepository
    {
        Task<bool> StoreApplication(long userId, ExpertApplicationRequestDTO requestDTO);
        bool HasPendingApplication(long userId);
        bool CheckIfPendingApplicationExist(long application);
        ICollection<ExpertApplication> GetExpertApplications(long userId);
        ICollection<ExpertApplication> GetPendingApplications();
        Task<bool> UpdateApplicationStatus(ExpertApplicationActionRequestDTO expertApplicationActionRequestDTO);
    }
}
