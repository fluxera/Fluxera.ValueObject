namespace Fluxera.ValueObject.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.ValueObject.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class PrimitiveValueObjectTests
	{
		private static IEnumerable<Action> PrimitiveTestData = new List<Action>
		{
			() => new StringValue(),
			() => new IntValue(),
			() => new GuidValue(),
			() => new EnumValue(),
		};

		[Test]
		[TestCaseSource(nameof(PrimitiveTestData))]
		public void ShouldNotThrowForValidTypes(Action action)
		{
			action.Should().NotThrow();
		}

		[Test]
		public void ShouldThrowForInvalidTypes()
		{
			Action action = () => new InvalidPrimitive();
			action.Should().Throw<TypeInitializationException>();
		}
	}
}
