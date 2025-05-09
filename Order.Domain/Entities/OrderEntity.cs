﻿using System;

namespace Order.Domain.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();
    }
}
