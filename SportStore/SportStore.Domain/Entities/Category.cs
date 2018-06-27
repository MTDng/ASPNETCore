using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace SportStore.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId {get; set;}
        public string CategoryName {get; set;}
        
        public virtual ICollection<Product> Products {get; set;}
        public Category()
        {
            Products = new HashSet<Product>();
        }
    }
}