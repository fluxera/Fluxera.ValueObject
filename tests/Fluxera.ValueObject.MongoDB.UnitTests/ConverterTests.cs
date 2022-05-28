namespace Fluxera.ValueObject.MongoDB.UnitTests
{
	using FluentAssertions;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using NUnit.Framework;

	[TestFixture]
	public class ConverterTests
	{
		static ConverterTests()
		{
			ConventionPack pack = new ConventionPack();
			pack.UsePrimitiveValueObject();
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		public class TestClass
		{
			public StringPrimitive StringPrimitive { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			StringPrimitive = new StringPrimitive("12345")
		};

		private static readonly string JsonString = @"{ ""StringPrimitive"" : ""12345"" }";

		[Test]
		public void ShouldDeserialize()
		{
			TestClass obj = BsonSerializer.Deserialize<TestClass>(JsonString);

			obj.StringPrimitive.Should().Be(new StringPrimitive("12345"));
		}

		[Test]
		public void ShouldSerialize()
		{
			string json = TestInstance.ToJson();

			json.Should().Be(JsonString);
		}
	}
}
