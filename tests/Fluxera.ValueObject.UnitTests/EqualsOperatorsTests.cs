﻿namespace Fluxera.ValueObject.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.ValueObject.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class EqualsOperatorsTests
	{
		private static IEnumerable<object[]> OperatorTestData = new List<object[]>
		{
			new object[]
			{
				null!,
				null!,
				true
			},
			new object[]
			{
				new Address("Testgasse", "50", "11111", "Bremen"),
				new Address("Testgasse", "50", "11111", "Bremen"),
				true
			},
			new object[]
			{
				new Address("Testgasse", "50", "11111", "Bremen"),
				new Address("Testweg", "50", "11111", "Bremen"),
				false
			},
			new object[]
			{
				new Address("Testgasse", "50", "11111", "Bremen"),
				null!,
				false
			}
		};

		private static IEnumerable<object[]> OperatorPrimitiveTestData = new List<object[]>
		{
			new object[]
			{
				new PostCode("12345"),
				new PostCode("12345"),
				true
			},
			new object[]
			{
				new PostCode("12345")
				{
					WillNotBeConsideredForEqualityAndHashCode = "ABC"
				},
				new PostCode("12345")
				{
					WillNotBeConsideredForEqualityAndHashCode = "ABC"
				},
				true
			},
			new object[]
			{
				new PostCode("12345")
				{
					WillNotBeConsideredForEqualityAndHashCode = "ABC"
				},
				new PostCode("12345")
				{
					WillNotBeConsideredForEqualityAndHashCode = "XYZ"
				},
				true
			},
			new object[]
			{
				new PostCode("12345")
				{
					WillNotBeConsideredForEqualityAndHashCode = "ABC"
				},
				new PostCode("54321")
				{
					WillNotBeConsideredForEqualityAndHashCode = "ABC"
				},
				false
			},
			new object[]
			{
				new PostCode("12345")
				{
					WillNotBeConsideredForEqualityAndHashCode = "ABC"
				},
				new PostCode("54321")
				{
					WillNotBeConsideredForEqualityAndHashCode = "XYZ"
				},
				false
			}
		};

		[Test]
		[TestCaseSource(nameof(OperatorPrimitiveTestData))]
		public void EqualOperatorPrimitiveShouldReturnExpectedValue(PostCode first, PostCode second, bool expected)
		{
			bool result = first == second;
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(OperatorTestData))]
		public void EqualOperatorShouldReturnExpectedValue(Address first, Address second, bool expected)
		{
			bool result = first == second;
			result.Should().Be(expected);
		}

		[Test]
		public void EqualOperatorShouldReturnFalseForDerivedTypes()
		{
			BankAccount first = new BankAccount("Tester", "DE0000000000000", "ABCDFFXXX");
			GermanBankAccount second = new GermanBankAccount("Tester", "DE0000000000000", "ABCDFFXXX");

			bool result = first == second;
			result.Should().BeFalse();
		}

		[Test]
		public void EqualOperatorShouldReturnFalseForDerivedTypesWithDifferentData()
		{
			GermanBankAccount first = new GermanBankAccount("Tester", "DE0000000000000", "ABCDFFXXX");
			GermanBankAccount second = new GermanBankAccount("Testonius", "DE0000000000000", "ABCDFFXXX");

			bool result = first == second;
			result.Should().BeFalse();
		}

		[Test]
		public void EqualOperatorShouldReturnTrueForDerivedTypesWithSameData()
		{
			GermanBankAccount first = new GermanBankAccount("Tester", "DE0000000000000", "ABCDFFXXX");
			GermanBankAccount second = new GermanBankAccount("Tester", "DE0000000000000", "ABCDFFXXX");

			bool result = first == second;
			result.Should().BeTrue();
		}

		[Test]
		public void EqualOperatorShouldReturnTrueForEmptyValueObject()
		{
			Empty first = new Empty();
			Empty second = new Empty();

			bool result = first == second;
			result.Should().BeTrue();
		}

		[Test]
		[TestCaseSource(nameof(OperatorTestData))]
		public void NotEqualOperatorPrimitiveShouldReturnExpectedValue(Address first, Address second, bool expected)
		{
			bool result = first != second;
			result.Should().Be(!expected);
		}

		[Test]
		[TestCaseSource(nameof(OperatorPrimitiveTestData))]
		public void NotEqualOperatorShouldReturnExpectedValue(PostCode first, PostCode second, bool expected)
		{
			bool result = first != second;
			result.Should().Be(!expected);
		}
	}
}
