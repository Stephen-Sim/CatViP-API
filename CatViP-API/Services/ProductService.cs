using AutoMapper;
using CatViP_API.DTOs.ProductDTOs;
using CatViP_API.Models;
using CatViP_API.Repositories;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services.Interfaces;

namespace CatViP_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ICollection<ProductTypeDTO> GetProductTypes()
        {
            return _mapper.Map<ICollection<ProductTypeDTO>>(_productRepository.GetProductTypes());
        }

        public object StoreProduct(long id, ProductRequestDTO productRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
