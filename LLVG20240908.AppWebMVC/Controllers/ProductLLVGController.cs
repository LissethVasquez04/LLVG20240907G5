using LLVG20240907.DTOs.ProductsDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LLVG20240907G5.AppWebMVC.Controllers
{
    public class ProductLLVGController : Controller
    {
        private readonly HttpClient _httpClientAPI;

        public ProductLLVGController(IHttpClientFactory httpClientFactory)
        {
            _httpClientAPI = httpClientFactory.CreateClient("API");
        }

        public async Task<IActionResult> Index(SearchQueryProductLLVGDTO searchQueryDTO, int countRow = 0)
        {
            if (searchQueryDTO.SendRowCount == 0)
                searchQueryDTO.SendRowCount = 2;

            if (searchQueryDTO.Take == 0)
                searchQueryDTO.Take = 10;

            var result = new SearchResultProductLLVGDTO();

            var response = await _httpClientAPI.PostAsJsonAsync("/product/search", searchQueryDTO);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<SearchResultProductLLVGDTO>();

            result = result ?? new SearchResultProductLLVGDTO();

            if (result.CountRow == 0 && searchQueryDTO.SendRowCount == 1)
                result.CountRow = countRow;

            ViewBag.CountRow = result.CountRow;
            searchQueryDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryDTO;

            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                // Redirige a la acción "Edit" si el ID es inválido.
                return RedirectToAction("Edit", new { id = id });
            }

            var result = new GetIdResultProductLLVGDTO();

            try
            {
                var response = await _httpClientAPI.GetAsync($"/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<GetIdResultProductLLVGDTO>();
                }

                // Si no se encuentra el producto, podrías redirigir a una página de error o a la lista
                if (result == null || result.Id <= 0)
                {
                    return RedirectToAction("Index");
                }

                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener los detalles: {ex.Message}";
                return View(new GetIdResultProductLLVGDTO());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductLLVGDTO createProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(createProductDTO);
            }

            try
            {
                var response = await _httpClientAPI.PostAsJsonAsync("/product", createProductDTO);

                if (response.IsSuccessStatusCode)
                {
                    var createdProduct = await response.Content.ReadFromJsonAsync<GetIdResultProductLLVGDTO>();
                    if (createdProduct != null)
                    {
                        return RedirectToAction(nameof(Index), new { id = createdProduct.Id });
                    }
                }

                ViewBag.Error = "Error al intentar guardar el registro";
                return View(createProductDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(createProductDTO);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0) return BadRequest("ID inválido");

            var result = new GetIdResultProductLLVGDTO();

            try
            {
                var response = await _httpClientAPI.GetAsync($"/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<GetIdResultProductLLVGDTO>();
                }

                return View(new EditProductLLVGDTO(result ?? new GetIdResultProductLLVGDTO()));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener los detalles para edición: {ex.Message}";
                return View(new EditProductLLVGDTO());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductLLVGDTO editProductDTO)
        {
            if (id <= 0) return BadRequest("ID inválido");

            try
            {
                var response = await _httpClientAPI.PutAsJsonAsync("/product", editProductDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar editar el registro";
                return View(editProductDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(editProductDTO);
            }


        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("ID inválido");

            var result = new GetIdResultProductLLVGDTO();

            try
            {
                var response = await _httpClientAPI.GetAsync($"/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<GetIdResultProductLLVGDTO>();
                }

                return View(result ?? new GetIdResultProductLLVGDTO());
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error al obtener el registro para eliminar: {ex.Message}";
                return View(new GetIdResultProductLLVGDTO());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GetIdResultProductLLVGDTO productDTO)
        {
            if (id <= 0) return BadRequest("ID inválido");

            try
            {
                var response = await _httpClientAPI.DeleteAsync($"/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar eliminar el registro";
                return View(productDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(productDTO);
            }
        }
    }
}
