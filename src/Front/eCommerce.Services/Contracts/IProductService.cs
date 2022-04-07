using eCommerce.Blazor.Demo.Common.Responses;
using eCommerce.Commons.Objects.Requests.Products;

namespace eCommerce.Services.Contracts
{
    public interface IProductService
    {
        public Task<ServiceRespConsumer> GetProductCatalog(ProductCatalogRequest request);

        public Task<ServiceRespConsumer> GetProduct(ProductRequest request);

        public Task<ServiceRespConsumer> GetProducts(ProductsRequest request);
    }
}
