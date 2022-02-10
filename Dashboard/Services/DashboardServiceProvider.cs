using CardApi;
using CustomerApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dashboard.Services
{
    public interface IDashboardServiceProvider
    {
        Task<List<Card>> GetCards();
        Task<Card> GetCard(string id);
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(string id);
    }

    public class DashboardServiceProvider: IDashboardServiceProvider
    {
        private readonly string _cardApiUrl;
        private readonly string _customerApiUrl;
        private readonly JsonSerializerOptions _options;

        public DashboardServiceProvider(string cardApiUrl, string customerApiUrl)
        {
            _cardApiUrl = cardApiUrl;
            _customerApiUrl = customerApiUrl;
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<List<Card>> GetCards()
        {
            using (var httpClient = new HttpClient())
            {
                var cardsJson = await httpClient.GetStringAsync(_cardApiUrl);

                return string.IsNullOrWhiteSpace(cardsJson) ? new List<Card>() : JsonSerializer.Deserialize<List<Card>>(cardsJson, _options);
            }
        }

        public async Task<Card> GetCard(string id)
        {
            using (var httpClient = new HttpClient())
            {
                var cardJson = await httpClient.GetStringAsync($"{_cardApiUrl}/{id}");

                return string.IsNullOrWhiteSpace(cardJson) ? null : JsonSerializer.Deserialize<Card>(cardJson, _options);
            }
        }

        public async Task<List<Customer>> GetCustomers()
        {
            using (var httpClient = new HttpClient())
            {
                var customersJson = await httpClient.GetStringAsync(_customerApiUrl);

                return string.IsNullOrWhiteSpace(customersJson) ? new List<Customer>() : JsonSerializer.Deserialize<List<Customer>>(customersJson, _options);
            }
        }

        public async Task<Customer> GetCustomer(string id)
        {
            using (var httpClient = new HttpClient())
            {
                var customerJson = await httpClient.GetStringAsync($"{_customerApiUrl}/{id}");

                return string.IsNullOrWhiteSpace(customerJson) ? null : JsonSerializer.Deserialize<Customer>(customerJson, _options);
            }
        }
    }
}
