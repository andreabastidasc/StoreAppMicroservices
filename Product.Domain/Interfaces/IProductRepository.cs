using System;

namespace Product.Domain.Interfaces
{
    using ProductEntity = Product.Domain.Entities.Product;

    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task<ProductEntity?> GetByIdAsync(Guid id);
        Task AddAsync(ProductEntity product);
        Task UpdateAsync(ProductEntity product);
        Task DeleteAsync(Guid id);
    }
}

