namespace Fluxera.ValueObject.UnitTests
{
	using FluentAssertions;
	using Fluxera.ValueObject.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ConversionOperatorTests
	{
		[Test]
		public void ShouldConvertImplicitlyToValue()
		{
			Age age = Age.Create(42);

			int result = age;

			result.Should().Be(age.Value);
		}

		[Test]
		public void ShouldConvertExplicitlyToValueObject()
		{
			Age result = (Age)42;

			result.Should().NotBeNull();
			result.Value.Should().Be(42);
		}
	}
}
