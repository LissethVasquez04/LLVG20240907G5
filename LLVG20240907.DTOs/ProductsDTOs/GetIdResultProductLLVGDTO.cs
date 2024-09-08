using System.ComponentModel.DataAnnotations;

namespace LLVG20240907.DTOs.ProductsDTOs
{
    public class GetIdResultProductLLVGDTO
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
