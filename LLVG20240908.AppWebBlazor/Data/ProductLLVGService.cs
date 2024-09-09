using LLVG20240907.DTOs.ProductsDTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LLVG20240907.API.Services
{
    public class ProductLLVGService
    {
        private readonly HttpClient _httpClient;

        // Constructor que recibe una instancia de IHttpClientFactory para crear el cliente HTTP
        public ProductLLVGService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        // Método para buscar productos utilizando una solicitud HTTP POST
        public async Task<SearchResultProductLLVGDTO> Search(SearchQueryProductLLVGDTO searchQueryProductDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("/product/search", searchQueryProductDTO);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SearchResultProductLLVGDTO>();
                return result ?? new SearchResultProductLLVGDTO();
            }
            return new SearchResultProductLLVGDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
        }

        // Método para obtener un producto por su ID utilizando una solicitud HTTP GET
        public async Task<GetIdResultProductLLVGDTO> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"/product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetIdResultProductLLVGDTO>();
                return result ?? new GetIdResultProductLLVGDTO();
            }
            return new GetIdResultProductLLVGDTO(); // Devolver un objeto vacío en caso de error o respuesta no exitosa
        }

        // Método para crear un nuevo producto utilizando una solicitud HTTP POST
        public async Task<int> Create(CreateProductLLVGDTO createProductDTO)
        {
            int result = 0;
            var response = await _httpClient.PostAsJsonAsync("/product", createProductDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

        // Método para editar un producto existente utilizando una solicitud HTTP PUT
        public async Task<int> Edit(EditProductLLVGDTO editProductDTO)
        {
            int result = 0;
            var response = await _httpClient.PutAsJsonAsync("/product", editProductDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

        // Método para eliminar un producto por su ID utilizando una solicitud HTTP DELETE
        public async Task<int> Delete(int id)
        {
            int result = 0;
            var response = await _httpClient.DeleteAsync($"/product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }
    }
}
