namespace Fluxera.ValueObject.UnitTests
{
	using FluentAssertions;
	using Fluxera.ValueObject.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ComparisonOperatorTests
	{
		[Test]
		public void ShouldCompareGreaterThan()
		{
			Age age = Age.Create(42);

			bool result = age > 20;

			result.Should().Be(true);
		}

		[Test]
		public void ShouldCompareLesserThan()
		{
			Age age = Age.Create(42);

			bool result = age < 20;

			result.Should().Be(false);
		}

		[Test]
		public void ShouldCompareGreaterEqualThan()
		{
			Age age = Age.Create(42);

			bool result = age >= 42;

			result.Should().Be(true);
		}

		[Test]
		public void ShouldCompareLesserEqualThan()
		{
			Age age = Age.Create(42);

			bool result = age <= 42;

			result.Should().Be(true);
		}
	}
}
