using System;
using System.Collections.Generic;

namespace WebClient.Dtos
{
    public class NewOrderDto
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<NewOrderItemDto> Items { get; set; }
    }

    public class NewOrderItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
