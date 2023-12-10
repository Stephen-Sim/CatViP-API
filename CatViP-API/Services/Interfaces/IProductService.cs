using CatViP_API.DTOs.ProductDTOs;

namespace CatViP_API.Services.Interfaces
{
    public interface IProductService
    {
        ICollection<ProductTypeDTO> GetProductTypes();
        object StoreProduct(long id, ProductRequestDTO productRequestDTO);
    }
}
