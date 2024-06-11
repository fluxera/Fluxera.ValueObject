namespace Fluxera.ValueObject.MongoDB.UnitTests.Model
{
	using global::MongoDB.Bson;

	public class Person
	{
		public ObjectId Id { get; set; }

		public string Name { get; set; }

		public Age Age { get; set; }
	}
}
