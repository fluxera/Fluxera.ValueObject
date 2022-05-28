namespace Fluxera.ValueObject.SystemTextJson.UnitTests
{
	using System.Text.Json;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class ConverterTests
	{
		private static readonly JsonSerializerOptions options;

		static ConverterTests()
		{
			options = new JsonSerializerOptions();
			options.UsePrimitiveValueObject();
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
			TestClass obj = JsonSerializer.Deserialize<TestClass>(JsonString, options);

			obj.StringPrimitive.Should().Be(new StringPrimitive("12345"));
		}

		[Test]
		public void ShouldSerialize()
		{
			string json = JsonSerializer.Serialize(TestInstance, options);

			json.Should().Be(JsonString);
		}
	}
}
