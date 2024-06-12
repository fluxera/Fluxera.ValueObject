namespace Fluxera.ValueObject.EntityFrameworkCore.UnitTests
{
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.ValueObject.EntityFrameworkCore.UnitTests.Model;
	using Fluxera.ValueObject.LiteDB.UnitTests.Model;
	using Microsoft.EntityFrameworkCore;
	using NUnit.Framework;

	[TestFixture]
	public class QueryTests
	{
#pragma warning disable NUnit1032
		private TestDbContext context;
#pragma warning restore NUnit1032

		[OneTimeSetUp]
		public void SetUp()
		{
			this.context = DbContextFactory.Generate(1);
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			this.context?.Dispose();
		}

		[Test]
		public async Task ShouldFindByPrimitiveValueObjectEquals()
		{
			Person linqFilterResult = await this.context
				.Set<Person>()
				.Where(x => x.Age == Age.Create(25))
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Ignore("Fix this later")]
		[Test]
		public async Task ShouldFindByValueEquals()
		{
			Person linqFilterResult = await this.context
				.Set<Person>()
				.Where(x => x.Age == 25)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Ignore("Fix this later")]
		[Test]
		public async Task ShouldFindByPrimitiveValueObjectComparison()
		{
			Person linqFilterResult = await this.context
				.Set<Person>()
				.Where(x => x.Age < Age.Create(40))
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Ignore("Fix this later")]
		[Test]
		public async Task ShouldFindByValueComparison()
		{
			Person linqFilterResult = await this.context
				.Set<Person>()
				.Where(x => x.Age < 40)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}
	}
}
