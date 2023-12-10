using CatViP_API.Data;
using CatViP_API.Models;

namespace CatViP_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatViPContext _context;

        public ProductRepository(CatViPContext context)
        {
            this._context = context;
        }

        public ICollection<ProductType> GetProductTypes()
        {
            return _context.ProductTypes.OrderBy(x => x.Name).ToList();
        }
    }
}
