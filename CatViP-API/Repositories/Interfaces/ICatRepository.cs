using CatViP_API.DTOs.CatDTOs;
using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface ICatRepository
    {
        ICollection<Cat> GetCats(long userId);
        Cat GetCat(long catId);
        Task<bool> StoreCat(long userId, CreateCatRequestDTO createCatRequestDTO);
        Task<bool> EditCat(EditCatRequestDTO editCatRequestDTO);
        Task<bool> DeleteCat(long catId);
        bool CheckIfCatExist(long catId);
    }
}

