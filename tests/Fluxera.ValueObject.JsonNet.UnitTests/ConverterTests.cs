namespace Fluxera.ValueObject.JsonNet.UnitTests
{
	using FluentAssertions;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class ConverterTests
	{
		[SetUp]
		public void SetUp()
		{
			JsonConvert.DefaultSettings = () =>
			{
				JsonSerializerSettings settings = new JsonSerializerSettings
				{
					Formatting = Formatting.None
				};
				settings.UsePrimitiveValueObject();
				return settings;
			};
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
			TestClass obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.StringPrimitive.Should().Be(new StringPrimitive("12345"));
		}

		[Test]
		public void ShouldSerialize()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}
	}
}
