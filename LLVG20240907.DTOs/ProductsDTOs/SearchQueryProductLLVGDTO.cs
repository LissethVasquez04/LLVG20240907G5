using System.ComponentModel.DataAnnotations;

namespace LLVG20240907.DTOs.ProductsDTOs
{
    public class SearchQueryProductLLVGDTO
    {
        [Display(Name = "Nombre")]
        public string? NombreLLVG_Like { get; set; }

        [Display(Name = "Página")]
        public int Skip { get; set; }

        [Display(Name = "CantReg X Página")]
        public int Take { get; set; }

        /// <summary>
        /// 1 = No se cuenta los resultados de la búsqueda
        /// 2 = Cuenta los resultados de la búsqueda
        /// </summary>
        public byte SendRowCount { get; set; }
    }

}
