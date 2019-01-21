using Microsoft.EntityFrameworkCore;

namespace pgConsoleApp
{
	public class BloggingContext : DbContext
	{
		public DbSet<Blog> Blogs { get; set; }

		public DbSet<Post> Posts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseNpgsql("Host=localhost;Database=my_pgConsoleApp;Username=postgres;Password=guest");
	}
}