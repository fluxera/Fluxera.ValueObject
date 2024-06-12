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
			() => new StringValue(string.Empty),
			() => new IntValue(0),
			() => new GuidValue(Guid.Empty)
		};

		[Test]
		[TestCaseSource(nameof(PrimitiveTestData))]
		public void ShouldNotThrowForValidTypes(Action action)
		{
			action.Should().NotThrow();
		}
	}
}
