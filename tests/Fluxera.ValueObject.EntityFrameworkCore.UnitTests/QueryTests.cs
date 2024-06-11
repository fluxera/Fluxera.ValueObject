namespace Fluxera.ValueObject.EntityFrameworkCore.UnitTests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.ValueObject.EntityFrameworkCore.UnitTests.Model;
	using Microsoft.EntityFrameworkCore;
	using NUnit.Framework;

	[TestFixture]
	public class QueryTests
	{
#pragma warning disable NUnit1032
		private TestDbContext context;
#pragma warning restore NUnit1032

		[SetUp]
		public void SetUp()
		{
			this.context = DbContextFactory.Generate(1);
		}

		[TearDown]
		public void TearDown()
		{
			this.context?.Dispose();
		}

		[Test]
		public async Task ShouldFindByPrimitiveValueObject()
		{
			Person linqFilterResult = await this.context
				.Set<Person>()
				.Where(x => x.Age.Value < 40)
				.FirstOrDefaultAsync();

			linqFilterResult.Should().NotBeNull();
		}
	}
}
