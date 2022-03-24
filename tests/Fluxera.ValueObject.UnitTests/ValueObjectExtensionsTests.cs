namespace Fluxera.ValueObject.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.ValueObject.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ValueObjectExtensionsTests
	{
		[Test]
		public void ShouldReturnFalseForObjectType()
		{
			Type type = typeof(object);
			type.IsValueObject().Should().BeFalse();
		}

		[Test]
		public void ShouldReturnTrueForPrimitiveValueObjectType()
		{
			Type type = typeof(PostCode);
			type.IsValueObject().Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForValueObjectDerivedType()
		{
			Type type = typeof(GermanBankAccount);
			type.IsValueObject().Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForValueObjectType()
		{
			Type type = typeof(Country);
			type.IsValueObject().Should().BeTrue();
		}
	}
}
