using Microsoft.EntityFrameworkCore;
using SportStore.Domain.Entities;

namespace SportStore.Domain.Concrete
{
	public class EntitiesContext : DbContext
	{
		public EntitiesContext(DbContextOptions<EntitiesContext> options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}