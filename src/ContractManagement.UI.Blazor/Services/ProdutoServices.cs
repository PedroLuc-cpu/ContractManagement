using ContractManagement.UI.Blazor.DTOs;
using System.Net.Http.Json;

namespace ContractManagement.UI.Blazor.Services
{
    public class ProdutoServices(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<ProdutoDto>> GetAllProdutos()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ProdutoDto>>("v1/api/produto");
            return response ?? [];
        }
    }
}
