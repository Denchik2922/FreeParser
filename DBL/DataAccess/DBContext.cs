using DBL.Models;
using Microsoft.EntityFrameworkCore;

namespace DBL.DataAccess
{
	public class DBContext : DbContext
	{
		public DBContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }

		public DbSet<Subscribe> Subscribes { get; set; }

		public DbSet<Permission> Permissions { get; set; }

		public DbSet<ExtraCategory> ExtraCategories { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<Burse> Burses { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
			.HasIndex(u => u.ClientId)
			.IsUnique();
		}
	}
}
