namespace Fluxera.ValueObject.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.ValueObject.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class GetHashCodeTests
	{
		private static IEnumerable<object[]> PrimitiveTestData = new List<object[]>
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

		private static IEnumerable<object[]> TestData = new List<object[]>
		{
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
				new BankAccount("Tester", "DE0000000000000", "ABCDFFXXX"),
				false
			},

			new object[]
			{
				new Empty(),
				new Empty(),
				true
			},

			new object[]
			{
				new Address("Testgasse", "50", "11111", "Bremen"),
				new Empty(),
				false
			},

			new object[]
			{
				new BankAccount("Tester", "DE0000000000000", "ABCDFFXXX"),
				new GermanBankAccount("Tester", "DE0000000000000", "ABCDFFXXX"),
				false
			},

			new object[]
			{
				new GermanBankAccount("Tester", "DE0000000000000", "ABCDFFXXX"),
				new GermanBankAccount("Tester", "DE0000000000000", "ABCDFFXXX"),
				true
			},

			new object[]
			{
				new GermanBankAccount("Tester", "DE0000000000000", "ABCDFFXXX"),
				new GermanBankAccount("Testonius", "DE0000000000000", "ABCDFFXXX"),
				false
			},
		};

		[Test]
		[TestCaseSource(nameof(TestData))]
		[TestCaseSource(nameof(PrimitiveTestData))]
		public void GetHashCodeShouldReturnExpectedValue(object first, object second, bool expected)
		{
			Console.WriteLine($"{first.GetHashCode()} : {second.GetHashCode()}");

			bool result = first.GetHashCode() == second.GetHashCode();
			result.Should().Be(expected);
		}
	}
}
