using LLVG20240907G5.API.Modelos.DAL;
using LLVG20240907G5.API.Modelos.EN;
using LLVG20240907.DTOs.ProductsDTOs;

namespace LLVG20240907G5.API.Endpoints
{
    public static class ProductEndpoint
    {
        public static void AddProductEndpoints(this WebApplication app)
        {
            app.MapPost("/product/search", async (SearchQueryProductLLVGDTO productDTO, ProductDAL productDAL) =>
            {
                var product = new ProductLLVG
                {
                    NombreLLVG = productDTO.NombreLLVG_Like
                };

                var products = await productDAL.Search(product, skip: productDTO.Skip, take: productDTO.Take);

                if (products == null || !products.Any())
                {
                    return Results.NotFound("No se encontraron productos con los criterios de búsqueda proporcionados.");
                }

                var productResult = new SearchResultProductLLVGDTO
                {
                    Data = products.Select(s => new SearchResultProductLLVGDTO.ProductLLVGDTO
                    {
                        Id = s.Id,
                        NombreLLVG = s.NombreLLVG,
                        DescripcionLLVG = s.DescripcionLLVG,
                        PrecioLLVG = s.PrecioLLVG
                    }).ToList(),
                    CountRow = productDTO.SendRowCount == 2 ? await productDAL.CountSearch(product) : products.Count
                };

                return Results.Ok(productResult);
            });

            app.MapGet("/product/{id}", async (int id, ProductDAL productDAL) =>
            {
                var product = await productDAL.GetById(id);

                if (product == null)
                {
                    return Results.NotFound("Producto no encontrado");
                }

                var productDTO = new GetIdResultProductLLVGDTO
                {
                    Id = product.Id,
                    NombreLLVG = product.NombreLLVG,
                    DescripcionLLVG = product.DescripcionLLVG,
                    PrecioLLVG = product.PrecioLLVG
                };
                return Results.Ok(productDTO);
            });

            app.MapPost("/product", async (CreateProductLLVGDTO productDTO, ProductDAL productDAL) =>
            {
                var product = new ProductLLVG
                {
                    NombreLLVG = productDTO.NombreLLVG,
                    DescripcionLLVG = productDTO.DescripcionLLVG,
                    PrecioLLVG = productDTO.PrecioLLVG
                };

                int result = await productDAL.Create(product);

                if (result > 0)
                {
                    return Results.Created($"/product/{result}", new { Id = result });
                }
                return Results.StatusCode(500);
            });

            app.MapPut("/product", async (EditProductLLVGDTO productDTO, ProductDAL productDAL) =>
            {
                var product = new ProductLLVG
                {
                    Id = productDTO.Id,
                    NombreLLVG = productDTO.NombreLLVG,
                    DescripcionLLVG = productDTO.DescripcionLLVG,
                    PrecioLLVG = productDTO.PrecioLLVG
                };

                int result = await productDAL.Edit(product);

                if (result > 0)
                {
                    return Results.Ok(result);
                }
                return Results.StatusCode(500);
            });

            app.MapDelete("/product/{id}", async (int id, ProductDAL productDAL) =>
            {
                if (id <= 0)
                {
                    return Results.BadRequest("ID inválido");
                }

                int result = await productDAL.Delete(id);

                if (result > 0)
                {
                    return Results.Ok("Producto eliminado exitosamente");
                }

                return Results.StatusCode(500);
            });
        }
    }
}
