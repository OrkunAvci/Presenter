using Microsoft.EntityFrameworkCore;
using Presenter.Model;

namespace Presenter.Data
{
	public class ImagesContext : DbContext
	{
		public DbSet<Images> Images { get; set; }

		public ImagesContext(DbContextOptions<ImagesContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Map entities to tables.
			modelBuilder.Entity<Images>().ToTable("images");

			//	Configure primary keys.
			modelBuilder.Entity<Images>().HasKey(i => i.ID).HasName("id");

			//	Configure columns.
			modelBuilder.Entity<Images>().Property(i => i.ID).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
			modelBuilder.Entity<Images>().Property(i => i.description).HasColumnType("nvarchar(100)").IsRequired();
			modelBuilder.Entity<Images>().Property(i => i.start).HasColumnType("datetime").IsRequired();
			modelBuilder.Entity<Images>().Property(i => i.finish).HasColumnType("datetime").IsRequired();
			modelBuilder.Entity<Images>().Property(i => i.link).HasColumnType("nvarchar(512)").IsRequired();
			modelBuilder.Entity<Images>().Property(i => i.screen_no).HasColumnType("int").IsRequired();
			modelBuilder.Entity<Images>().Property(i => i.is_video).HasColumnType("tinyint(1)").IsRequired();
		}
	}
}
