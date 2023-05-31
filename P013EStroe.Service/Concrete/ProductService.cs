using P013EStroe.Data;
using P013EStroe.Data.Concrete;
using P013EStroe.Service.Abstract;

namespace P013EStroe.Service.Concrete
{
    public class ProductService : ProductRepository, IProductService
    {
        public ProductService(DatabaseContext context) : base(context)
        {
        }
    }
}
