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

		[SetUp]
		public void SetUp()
		{
			BsonMapper.Global.Entity<Person>().Id(x => x.Id);
			BsonMapper.Global.UsePrimitiveValueObject();

			this.database = new LiteDatabaseAsync($"{Guid.NewGuid():N}.db");
		}

		[TearDown]
		public void TearDown()
		{
			this.database?.Dispose();
		}

		[Test]
		public async Task ShouldFindByPrimitiveValueObject()
		{
			ILiteCollectionAsync<Person> collection = this.database.GetCollection<Person>();

			Person person = new Person
			{
				Name = "Tester",
				Age = Age.Create(25),
			};

			await collection.InsertAsync(person);

			Person linqFilterResult = await collection
				.AsQueryable()
				.Where(x => x.Age.Value < 40)
				.FirstOrDefaultAsync();

			linqFilterResult.Should().NotBeNull();
		}
	}
}
