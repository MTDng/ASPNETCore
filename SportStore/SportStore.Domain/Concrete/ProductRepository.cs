using System.Collections.Generic;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.Domain.Concrete;

namespace SportStore.Domain.Concrete
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private EntitiesContext context;
        
        public ProductRepository(EntitiesContext context): base(context)
        {
            this.context = context;
        }
        public Product GetProductsByName(string productName)
        {
            return context.Products.Find(productName);
        }
    }
}