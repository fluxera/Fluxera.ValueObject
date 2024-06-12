namespace Fluxera.ValueObject.MongoDB.UnitTests
{
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.ValueObject.MongoDB.UnitTests.Model;
	using global::MongoDB.Bson.Serialization.Conventions;
	using global::MongoDB.Driver;
	using global::MongoDB.Driver.Linq;
	using NUnit.Framework;

	[TestFixture]
	public class QueryTests
	{
		private IMongoCollection<Person> collection;

		[OneTimeSetUp]
		public async Task Setup()
		{
			ConventionPack pack = [];
			pack.UsePrimitiveValueObject();
			ConventionRegistry.Register("ConventionPack", pack, t => true);

			IMongoClient client = new MongoClient(GlobalFixture.ConnectionString);
			IMongoDatabase database = client.GetDatabase(GlobalFixture.Database);
			this.collection = database.GetCollection<Person>("People");

			Person person = new Person
			{
				Name = "Tester",
				Age = Age.Create(25),
			};

			await this.collection.InsertOneAsync(person);
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
