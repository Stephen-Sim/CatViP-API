using CatViP_API.Data;
using CatViP_API.DTOs.ProductDTOs;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatViP_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatViPContext _context;

        public ProductRepository(CatViPContext context)
        {
            this._context = context;
        }

        public bool CheckProductExist(long authId, long productId)
        {
            return _context.Products.Any(x => x.SellerId == authId && x.Id == productId);
        }

        public Product GetProduct(long id)
        {
            return _context.Products.Include(x => x.ProductType).FirstOrDefault(x => x.Id == id)!;
        }

        public ICollection<Product> GetProducts(long authId)
        {
            return _context.Products.Where(x => x.SellerId == authId).Include(x => x.ProductType).ToList();
        }

        public ICollection<ProductType> GetProductTypes()
        {
            return _context.ProductTypes.OrderBy(x => x.Name).ToList();
        }

        public async Task<bool> RemoveProduct(long id)
        {
            try
            {
                _context.Remove(_context.Products.FirstOrDefault(x => x.Id == id)!);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> StoreProduct(Product product)
        {
            try
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateProduct(long id, ProductEditRequestDTO productRequestDTO)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);
                product!.Description = productRequestDTO.Description;
                product!.Name = productRequestDTO.Name;
                product!.Price = productRequestDTO.Price;
                product!.ProductTypeId = productRequestDTO.ProductTypeId;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
