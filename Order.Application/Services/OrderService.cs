using Order.Application.DTOs;
using Order.Domain.Entities;
using Order.Domain.Interfaces;
using Order.Application.Clients;

namespace Order.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ProductClient _productClient;


        public OrderService(IOrderRepository repository, ProductClient productClient)
        {
            _repository = repository;
            _productClient = productClient;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _repository.GetAllAsync();

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.CustomerName,
                OrderDate = DateTime.UtcNow,
                TotalAmount = o.TotalAmount,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            });
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };
        }

        public async Task CreateAsync(OrderDto dto)
        {
            decimal total = 0;

            foreach (var item in dto.Items)
            {
                var product = await _productClient.GetProductByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"Producto con ID {item.ProductId} no encontrado.");

                if (item.Quantity > product.Stock)
                    throw new Exception($"Stock insuficiente para {product.Name}. Disponible: {product.Stock}");

                item.ProductName = product.Name;
                item.UnitPrice = product.Price;
                item.Subtotal = item.UnitPrice * item.Quantity;

                total += item.Subtotal;

                // Actualizar stock
                int newStock = product.Stock - item.Quantity;
                var updated = await _productClient.UpdateProductStockAsync(product.Id, newStock);
                if (!updated)
                    throw new Exception($"Error actualizando stock para producto {product.Name}");
            }

            dto.TotalAmount = total;
            dto.OrderDate = DateTime.UtcNow;

            // crear entidad
            var order = new OrderEntity
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                CustomerName = dto.CustomerName,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.TotalAmount,
                Items = dto.Items.Select(i => new OrderItemEntity
                {
                    Id = Guid.NewGuid(),
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _repository.AddAsync(order);
        }

        public async Task UpdateAsync(OrderDto dto)
        {
            var order = await _repository.GetByIdAsync(dto.Id);
            if (order == null) throw new Exception("Order not found");

            // get original stock
            var originalItems = order.Items.ToDictionary(i => i.ProductId, i => i.Quantity);

            foreach (var newItem in dto.Items)
            {
                var product = await _productClient.GetProductByIdAsync(newItem.ProductId);
                if (product == null) throw new Exception($"Producto {newItem.ProductId} no encontrado");

                var originalQty = originalItems.ContainsKey(newItem.ProductId) ? originalItems[newItem.ProductId] : 0;
                var qtyDifference = newItem.Quantity - originalQty;

                var newStock = product.Stock - qtyDifference;
                if (newStock < 0) throw new Exception($"Stock insuficiente para producto {product.Name}");

                var updated = await _productClient.UpdateProductStockAsync(product.Id, newStock);
                if (!updated) throw new Exception($"Error actualizando stock para producto {product.Name}");
            }

            // update order
            order.CustomerId = dto.CustomerId;
            order.CustomerName = dto.CustomerName;
            order.OrderDate = dto.OrderDate;
            order.TotalAmount = dto.TotalAmount;

            order.Items = dto.Items.Select(i => new OrderItemEntity
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList();

            await _repository.UpdateAsync(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null) throw new Exception("Order not found");

            foreach (var item in order.Items)
            {
                var product = await _productClient.GetProductByIdAsync(item.ProductId);
                if (product != null)
                {
                    var newStock = product.Stock + item.Quantity;
                    var updated = await _productClient.UpdateProductStockAsync(product.Id, newStock);
                    if (!updated) throw new Exception($"Error actualizando stock para producto {product.Name}");
                }
            }

            await _repository.DeleteAsync(id);
        }
    }
}
