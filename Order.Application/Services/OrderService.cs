using Order.Application.DTOs;
using Order.Domain.Entities;
using Order.Domain.Interfaces;

namespace Order.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
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
            await _repository.DeleteAsync(id);
        }

    }
}
