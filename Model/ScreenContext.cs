using Microsoft.EntityFrameworkCore;
using Presenter.Model;

namespace Presenter.Data
{
	public class ScreenContext : DbContext
	{
		public DbSet<Screen> Screens { get; set; }

		public ScreenContext(DbContextOptions<ScreenContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Map entities to tables.
			modelBuilder.Entity<Screen>().ToTable("screen");

			//	Configure primary keys.
			modelBuilder.Entity<Screen>().HasKey(s => s.ID).HasName("id");

			//	Configure columns.
			modelBuilder.Entity<Screen>().Property(s => s.ID).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
			modelBuilder.Entity<Screen>().Property(s => s.description).HasColumnType("nvarchar(256)").IsRequired();
			modelBuilder.Entity<Screen>().Property(s => s.location).HasColumnType("nvarchar(256)").IsRequired();
			modelBuilder.Entity<Screen>().Property(s => s.refresh).HasColumnType("uint").IsRequired();
		}
	}
}
