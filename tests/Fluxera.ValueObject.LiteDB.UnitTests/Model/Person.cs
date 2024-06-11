namespace Fluxera.ValueObject.LiteDB.UnitTests.Model
{
	using global::LiteDB;

	public class Person
	{
		public ObjectId Id { get; set; }

		public string Name { get; set; }

		public Age Age { get; set; }
	}
}
