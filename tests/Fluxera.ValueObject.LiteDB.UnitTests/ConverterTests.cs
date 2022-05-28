namespace Fluxera.ValueObject.LiteDB.UnitTests
{
	using FluentAssertions;
	using global::LiteDB;
	using NUnit.Framework;

	[TestFixture]
	public class ConverterTests
	{
		static ConverterTests()
		{
			BsonMapper.Global.UsePrimitiveValueObject();
		}

		public class TestClass
		{
			public StringPrimitive StringPrimitive { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			StringPrimitive = new StringPrimitive("12345")
		};

		private static readonly string JsonString = @"{""StringPrimitive"":""12345""}";

		[Test]
		public void ShouldDeserialize()
		{
			BsonDocument doc = (BsonDocument)JsonSerializer.Deserialize(JsonString);
			TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.StringPrimitive.Should().Be(new StringPrimitive("12345"));
		}

		[Test]
		public void ShouldSerialize()
		{
			BsonDocument doc = BsonMapper.Global.ToDocument(TestInstance);
			string json = JsonSerializer.Serialize(doc);

			json.Should().Be(JsonString);
		}
	}
}
