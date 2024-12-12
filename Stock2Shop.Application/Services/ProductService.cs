using Stock2Shop.Core.Models;
using Stock2Shop.Core.Repositories;
using Stock2Shop.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock2Shop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddProductsAsync(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                if (await _repository.ExistsAsync(product.Sku))
                {
                    throw new System.Exception($"Product with SKU '{product.Sku}' already exists.");
                }

                await _repository.AddAsync(product);
            }
        }
    }
}
