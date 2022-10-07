using DataGenerator.Model;
using Microsoft.EntityFrameworkCore;


namespace DataGenerator.Repository;

internal class DataGeneratorDbContext : DbContext
{
	public DbSet<Asset> Assets { get; set; }

	public DbSet<Project> Projects { get; set; }

	public DbSet<Client> Clients { get; set; }

	public DataGeneratorDbContext()
	{

#if DEBUG
		this.Database.EnsureDeleted();
#endif

		this.Database.EnsureCreated();
	}

	/// <summary>
	/// Setup the database configuration in our case an sqlite database.
	/// </summary>
	/// <param name="builder"></param>
	protected override void OnConfiguring(DbContextOptionsBuilder builder)
	{
		builder.UseSqlite("Data Source=datagenerator.db");
	}

	/// <summary>
	/// Configuring our tables and in this case seeding our tables.
	/// </summary>
	/// <param name="modelBuilder"></param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

#if DEBUG
		Seed seed = new();

		modelBuilder.Entity<Asset>().HasData(seed.Assets);
		modelBuilder.Entity<Project>().HasData(seed.Projects);
		modelBuilder.Entity<Client>().HasData(seed.Clients);
#endif

	}
}
