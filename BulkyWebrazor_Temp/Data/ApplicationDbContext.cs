using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebrazor_Temp.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
		{
		
			
		}

		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "A", DisplayOrder = 3 },
				new Category { Id = 1, Name = "B", DisplayOrder = 5 },
				new Category { Id = 3, Name = "C", DisplayOrder = 2 }
		);
		}
	}
}
