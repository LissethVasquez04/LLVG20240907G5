using Microsoft.EntityFrameworkCore;

namespace LLVG20240907G5.Modelos.EN
{
    public class CRMContext:DbContext
    {
        public CRMContext(DbContextOptions<CRMContext> options) : base(options)
        {
        }
        public DbSet<ProductoLLVG> ProductoLLVG { get; set; }
    }
}
