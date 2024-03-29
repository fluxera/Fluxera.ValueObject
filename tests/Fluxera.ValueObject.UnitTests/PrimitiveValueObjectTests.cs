﻿namespace Fluxera.ValueObject.UnitTests
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
			() => new GuidValue(Guid.Empty),
			() => new EnumValue(Currency.Dollar),
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
			Action action = () => new InvalidPrimitive(new Address("1", "2", "3", "4"));
			action.Should().Throw<TypeInitializationException>();
		}
	}
}
