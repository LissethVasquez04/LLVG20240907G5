using System.ComponentModel.DataAnnotations;

namespace LLVG20240907.DTOs.ProductsDTOs
{
    public class EditProductLLVGDTO
    {
        [Required(ErrorMessage = "El campo ID es obligatorio.")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        public string NombreLLVG { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El campo Descripción no puede tener más de 255 caracteres.")]
        public string DescripcionLLVG { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Precio debe ser un número positivo.")]
        public decimal PrecioLLVG { get; set; }

        public EditProductLLVGDTO()
        {
            NombreLLVG = string.Empty;
            DescripcionLLVG = string.Empty;
            PrecioLLVG = 0.01m;
        }

        public EditProductLLVGDTO(GetIdResultProductLLVGDTO getIdResultProductLLVGDTO)
        {
            Id = getIdResultProductLLVGDTO.Id;
            NombreLLVG = getIdResultProductLLVGDTO.NombreLLVG;
            DescripcionLLVG = getIdResultProductLLVGDTO.DescripcionLLVG;
            PrecioLLVG = getIdResultProductLLVGDTO.PrecioLLVG;
        }
    }

}
