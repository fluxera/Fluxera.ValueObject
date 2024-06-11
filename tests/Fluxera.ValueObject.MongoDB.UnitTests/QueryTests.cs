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
		[Test]
		public async Task ShouldFindByPrimitiveValueObject()
		{
			ConventionPack pack = [];
			pack.UsePrimitiveValueObject();
			ConventionRegistry.Register("ConventionPack", pack, t => true);

			IMongoClient client = new MongoClient(GlobalFixture.ConnectionString);
			IMongoDatabase database = client.GetDatabase(GlobalFixture.Database);
			IMongoCollection<Person> collection = database.GetCollection<Person>("People");

			Person person = new Person
			{
				Name = "Tester",
				Age = Age.Create(25),
			};

			await collection.InsertOneAsync(person);

			Person stringFilterResult = await collection
			   .Find(Builders<Person>.Filter.Lt("Age.Value", 40))
			   .FirstOrDefaultAsync();
			stringFilterResult.Should().NotBeNull();

			Person expressionFilterResult = await collection
				.Find(Builders<Person>.Filter.Lt(p => p.Age.Value, 40))
				.FirstOrDefaultAsync();
			expressionFilterResult.Should().NotBeNull();

			Person linqFilterResult = await collection
				.AsQueryable()
				.Where(x => x.Age.Value < 40)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}
	}
}
