using System.ComponentModel.DataAnnotations;

namespace LLVG20240907.DTOs.ProductsDTOs
{
    public class SearchResultProductLLVGDTO
    {
        public int CountRow { get; set; }
        public List<ProductLLVGDTO> Data { get; set; }

        public class ProductLLVGDTO
        {
            public int Id { get; set; }
            [Display(Name = "Nombre")]
            public string NombreLLVG { get; set; }
            [Display(Name = "Descripción")]
            public string DescripcionLLVG { get; set; }
            [Display(Name = "Precio")]
            public decimal PrecioLLVG { get; set; }
        }
    }

}
