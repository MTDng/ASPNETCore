using Microsoft.EntityFrameworkCore;
using SportStore.Domain.Entities;

namespace SportStore.Domain.Concrete
{
	public class SportStoreEntities : DbContext
	{
		public SportStoreEntities(DbContextOptions<SportStoreEntities> options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}