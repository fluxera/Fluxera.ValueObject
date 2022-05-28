namespace Fluxera.ValueObject.EntityFrameworkCore.UnitTests
{
	using System.Collections.Generic;
	using System.Linq;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class ModelBuilderExtensionsTests
	{
		[Test]
		public void ShouldUseNameConverter()
		{
			int seedCount = 1;
			using TestDbContext context = DbContextFactory.Generate(seedCount);
			List<Person> people = context.Set<Person>().ToList();

			people.Should().BeEquivalentTo(context.SeedData);
		}
	}
}
