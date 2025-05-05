using System.Net.Http.Json;
using System.Threading.Tasks;
using Identity.Application.DTOs;

namespace Identity.Application.Clients
{
    public class CustomerClient
    {
        private readonly HttpClient _httpClient;

        public CustomerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerDto?> CreateCustomerAsync(CustomerDto customerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customers", customerDto);

            if (response.IsSuccessStatusCode)
            {
                var createdCustomer = await response.Content.ReadFromJsonAsync<CustomerDto>();
                return createdCustomer;
            }

            return null;
        }
    }
}
