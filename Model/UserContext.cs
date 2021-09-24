using Microsoft.EntityFrameworkCore;
using Presenter.Model;

namespace Presenter.Data
{
	public class UserContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public UserContext(DbContextOptions<UserContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Map entities to tables.
			modelBuilder.Entity<User>().ToTable("user");

			//	Configure primary keys.
			modelBuilder.Entity<User>().HasKey(u => u.ID).HasName("id");

			//	Configure columns.
			modelBuilder.Entity<User>().Property(u => u.ID).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
			modelBuilder.Entity<User>().Property(u => u.username).HasColumnType("nvarchar(64)").IsRequired();
			modelBuilder.Entity<User>().Property(u => u.password).HasColumnType("nvarchar(64)").IsRequired();
		}

	}
}
