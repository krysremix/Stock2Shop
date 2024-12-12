using Stock2Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock2Shop.Core.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetBySkuAsync(string sku);
        Task AddAsync(Product product);
        Task<bool> ExistsAsync(string sku);
    }
}
