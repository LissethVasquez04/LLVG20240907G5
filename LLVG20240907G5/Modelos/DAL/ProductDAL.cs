using Microsoft.EntityFrameworkCore;
using LLVG20240907G5.API.Modelos.EN;

namespace LLVG20240907G5.API.Modelos.DAL
{
    public class ProductDAL
    {
        private readonly CRMContext _context;

        public ProductDAL(CRMContext context)
        {
            _context = context;
        }

        public async Task<int> Create(ProductLLVG product)
        {
            _context.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductLLVG> GetById(int id)
        {
            var product = await _context.ProductLLVG.FirstOrDefaultAsync(p => p.Id == id);
            return product ?? new ProductLLVG(); // Return a new instance if not found
        }

        public async Task<int> Edit(ProductLLVG product)
        {
            var productToUpdate = await GetById(product.Id);
            if (productToUpdate.Id != 0)
            {
                // Update product details
                productToUpdate.NombreLLVG = product.NombreLLVG;
                productToUpdate.DescripcionLLVG = product.DescripcionLLVG;
                productToUpdate.PrecioLLVG = product.PrecioLLVG;
                return await _context.SaveChangesAsync();
            }
            return 0; // No update performed
        }

        public async Task<int> Delete(int id)
        {
            var productToDelete = await GetById(id);
            if (productToDelete.Id > 0)
            {
                _context.ProductLLVG.Remove(productToDelete);
                return await _context.SaveChangesAsync();
            }
            return 0; // No deletion performed
        }

        private IQueryable<ProductLLVG> Query(ProductLLVG product)
        {
            var query = _context.ProductLLVG.AsQueryable();

            if (!string.IsNullOrWhiteSpace(product.NombreLLVG))
            {
                query = query.Where(p => p.NombreLLVG.Contains(product.NombreLLVG));
            }

            return query;
        }

        public async Task<int> CountSearch(ProductLLVG product)
        {
            return await Query(product).CountAsync();
        }

        public async Task<List<ProductLLVG>> Search(ProductLLVG product, int take = 10, int skip = 0)
        {
            take = take > 0 ? take : 10; // Ensure take is positive
            var query = Query(product);
            query = query.OrderByDescending(p => p.Id).Skip(skip).Take(take);
            return await query.ToListAsync();
        }
    }
}
