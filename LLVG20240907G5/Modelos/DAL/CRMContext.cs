using LLVG20240907G5.API.Modelos.EN;
using Microsoft.EntityFrameworkCore;

namespace LLVG20240907G5.API.Modelos.DAL
{
    public class CRMContext : DbContext
    {
        public CRMContext(DbContextOptions<CRMContext> options) : base(options)
        {
        }
        public DbSet<ProductLLVG> ProductLLVG { get; set; }
    }
}
