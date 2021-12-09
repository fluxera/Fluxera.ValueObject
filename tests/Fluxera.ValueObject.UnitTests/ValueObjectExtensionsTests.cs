namespace Fluxera.ValueObject.UnitTests
{
	using System;
	using FluentAssertions;
	using Model;
	using NUnit.Framework;

	[TestFixture]
	public class ValueObjectExtensionsTests
	{
		[Test]
		public void ShouldReturnTrueForValueObjectType()
		{
			Type type = typeof(Country);
			type.IsValueObject().Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForValueObjectDerivedType()
		{
			Type type = typeof(GermanBankAccount);
			type.IsValueObject().Should().BeTrue();
		}

		[Test]
		public void ShouldReturnFalseForObjectType()
		{
			Type type = typeof(object);
			type.IsValueObject().Should().BeFalse();
		}
	}
}
