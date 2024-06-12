namespace Fluxera.ValueObject.LiteDB.UnitTests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.ValueObject.LiteDB.UnitTests.Model;
	using global::LiteDB;
	using global::LiteDB.Async;
	using global::LiteDB.Queryable;
	using NUnit.Framework;

	[TestFixture]
	public class QueryTests
	{
		private LiteDatabaseAsync database;
		private ILiteCollectionAsync<Person> collection;

		[OneTimeSetUp]
		public async Task SetUp()
		{
			BsonMapper.Global.Entity<Person>().Id(x => x.Id);
			BsonMapper.Global.UsePrimitiveValueObject();

			this.database = new LiteDatabaseAsync($"{Guid.NewGuid():N}.db");

			this.collection = this.database.GetCollection<Person>();

			Person person = new Person
			{
				Name = "Tester",
				Age = Age.Create(25),
			};

			await collection.InsertAsync(person);
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			this.database?.Dispose();
		}

		[Test]
		public async Task ShouldFindByPrimitiveValueObjectEquals()
		{
			Person linqFilterResult = await this.collection
				.AsQueryable()
				.Where(x => x.Age == Age.Create(25))
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Test]
		public async Task ShouldFindByValueEquals()
		{
			Person linqFilterResult = await this.collection
				.AsQueryable()
				.Where(x => x.Age == 25)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Test]
		public async Task ShouldFindByPrimitiveValueObjectComparison()
		{
			Person linqFilterResult = await this.collection
				.AsQueryable()
				.Where(x => x.Age < Age.Create(40))
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Test]
		public async Task ShouldFindByValueComparison()
		{
			Person linqFilterResult = await this.collection
				.AsQueryable()
				.Where(x => x.Age < 40)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}
	}
}
