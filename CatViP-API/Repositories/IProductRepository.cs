using CatViP_API.Models;

namespace CatViP_API.Repositories
{
    public interface IProductRepository
    {
        ICollection<ProductType> GetProductTypes();
    }
}
