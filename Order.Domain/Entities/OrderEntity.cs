using System;

namespace Order.Domain.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
}
