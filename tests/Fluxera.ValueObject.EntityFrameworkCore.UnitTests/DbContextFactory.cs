namespace Fluxera.ValueObject.EntityFrameworkCore.UnitTests
{
	using System.Linq;
	using Fluxera.ValueObject.EntityFrameworkCore.UnitTests.Model;

	public static class DbContextFactory
	{
		public static TestDbContext Generate(int seedCount)
		{
			PersonFactory.Initialize();

			TestDbContext context = new TestDbContext
			{
				SeedData = PersonFactory.Generate(seedCount).ToArray(),
			};

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}
	}
}
