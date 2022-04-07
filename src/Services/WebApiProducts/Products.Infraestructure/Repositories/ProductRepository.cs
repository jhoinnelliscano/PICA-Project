using eCommerce.Commons.Utilities;
using Microsoft.EntityFrameworkCore;
using Products.Core.Config;
using Products.Core.Contracts.Repositories;
using Products.Core.Objects.DbTypes;
using Products.Infraestructure.Contexts.DbProduct;
using Products.Infraestructure.DbHelpers;

namespace Products.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbProductsContext _dbcontext;
        private readonly IDbProductHelper _dbProductHelper;

        public ProductRepository(DbProductsContext dbcontext, IDbProductHelper dbProductHelper)
        {
            _dbcontext = dbcontext;
            _dbProductHelper = dbProductHelper;
        }

        public async Task<ProductEntity> GetProductById(long productId)
        {
            var bdProdcut = await _dbcontext.Products
                                    .Include(x => x.ProductCategory)
                                    .Include(x => x.ProductProvider)
                                    .Include(x => x.ProductDetails)
                                    .Include(x => x.ProductSpecifications)
                                    .Include(x => x.ProductReviews)
                                    .Include(x => x.ProductImages)
                                    .FirstOrDefaultAsync(x=>x.Id == productId);

            if (bdProdcut == null) return null;

            return _dbProductHelper.ConvertToProductEntity(bdProdcut);
        }

        public ProductCatalogEntity GetProducts(int categoryId, int providerId, string search, int page, 
            int itemsByPage, ProductUtilities.ORDERBY sort, decimal minPrice, decimal maxPrice, ProductUtilities.CONDITIONCODE condition)
        {
            var filter = _dbProductHelper.FiltersExpression(search, categoryId, providerId, minPrice, maxPrice, condition);
            var orderBy = _dbProductHelper.OrderBy(sort);

            IQueryable<Product> query = _dbcontext.Products;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            var includeProperties = "ProductCategory,ProductProvider,ProductDetails,ProductSpecifications,ProductReviews";
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            var skip = (page - 1) * itemsByPage;

            var count = orderBy?.Invoke(query).Count() ?? query.Count();
            var MaxValue = Convert.ToInt32(AppConfiguration.Configuration["AppConfiguration:MaxItemsByPage"].ToString());
            var result = count <= MaxValue ? orderBy?.Invoke(query).ToList() ?? query.ToList()
                : orderBy?.Invoke(query).Skip(skip).Take(itemsByPage).ToList() ?? query.Skip(skip).Take(itemsByPage).ToList();
            var resultProducts  = result.Select(x => _dbProductHelper.ConvertToProductEntity(x)).ToList();

            return new ProductCatalogEntity(resultProducts, count);
        }

        public IEnumerable<ProductEntity> GetProductsById(IEnumerable<long> productsId) 
        {
            var bdProdcuts = _dbcontext.Products
                                    .Include(x => x.ProductCategory)
                                    .Include(x => x.ProductProvider)
                                    .Include(x => x.ProductDetails)
                                    .Include(x => x.ProductSpecifications)
                                    .Include(x => x.ProductReviews)
                                    .Where(x => productsId.Any(y => y == x.Id));

            return _dbProductHelper.ConvertToProductEntity(bdProdcuts);
        }
    }
}
