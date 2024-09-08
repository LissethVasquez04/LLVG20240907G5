using Microsoft.EntityFrameworkCore;
using LLVG20240907G5.Modelos.EN;
namespace LLVG20240907G5.Modelos.DAL
{
    public class ProductDAL
    {
        readonly CRMContext _context;

        public ProductDAL(CRMContext context)
        {
            _context = context;
        }

        public async Task<int> create(ProductLLVG productLLVG)
        {
            _context.Add(productLLVG);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductLLVG> GetById(int id)
        {
            var product = await _context.ProductLLVG.FirstOrDefaultAsync(p => p.id == id);
            return product != null ? product : new ProductLLVG();
        }

        public async Task<int> Edit(ProductLLVG productLLVG)
        {
            int result = 0;
            var productUodate = await GetById(ProductLLVG.id);
            if (productUodate.id != 0)
            {
                productUodate.NombreLLVG = ProductLLVG.NombreLLVG;
                productUodate.DescripcionLLVG = ProductLLVG.DescripcionLLVG;
                productUodate.PrecioLLVG = ProductLLVG.PrecioLLVG;
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var productDelete = await GetById(id);
            if (productDelete.id > 0)
            {
                _context.ProductLLVG.Remove(productDelete);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        private IQueryable<ProductLLVG> Query(ProductLLVG productLLVG)
        {
            var query = _context.ProductLLVG.AsQueryable();
            if (!string.IsNullOrWhiteSpace(productLLVG.NombreLLVG))
                query = query.Where(s => s.NombreLLVG.Contains(productLLVG.NombreLLVG));
            return query;
        }

        public async Task<int> CountSearch(ProductLLVG productLLVG)
        {
            return await Query(productLLVG).CountAsync();
        }

        public async Task<List<ProductLLVG>> Seacrh(ProductLLVG productLLVG, int take = 10, int skip = 0)
        {
            take = take == 0 ? 10 : take;
            var query = Query(productLLVG);
            query = query.OrderByDescending(s => s.id).Skip(skip).Take(take);
            return await query.ToListAsync();
        }
    }
}
}
