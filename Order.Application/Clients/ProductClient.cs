using System.Net.Http.Json;
using Order.Application.DTOs;

namespace Order.Application.Clients
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{productId}");
        }

        public async Task<bool> UpdateProductStockAsync(Guid productId, int newStock)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null) return false;

            product.Stock = newStock;
            var response = await _httpClient.PutAsJsonAsync($"api/products/{productId}", product);
            return response.IsSuccessStatusCode;
        }
    }
}