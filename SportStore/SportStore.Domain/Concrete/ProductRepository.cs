using System.Collections.Generic;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;

namespace SportStore.Domain.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private EntitiesContext context;
        
        public ProductRepository(EntitiesContext context)
        {
            this.context = context;
        }
        public Product GetProductsByName(string productName)
        {
            return context.Products.Find(productName);
        }
    }
}