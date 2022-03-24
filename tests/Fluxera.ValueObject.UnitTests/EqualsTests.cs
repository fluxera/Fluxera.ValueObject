namespace Fluxera.ValueObject.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.ValueObject.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class EqualsTests
	{
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
				null!,
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
				new PostCode("12345"),
				new PostCode("54321"),
				false
			}
		};

		[Test]
		[TestCaseSource(nameof(TestData))]
		[TestCaseSource(nameof(PrimitiveTestData))]
		public void EqualShouldReturnExpectedValue(object first, object second, bool expected)
		{
			bool result = first.Equals(second);
			result.Should().Be(expected);
		}

		[Test]
		public void EqualShouldReturnTrueForCorrectlyImplementedCollections()
		{
			Confederation confederation1 = new Confederation("European Union", new List<Country>
			{
				Country.Create("FR"),
				Country.Create("DE"),
				Country.Create("AT"),
				// ...
			});

			Confederation confederation2 = new Confederation("European Union", new List<Country>
			{
				Country.Create("FR"),
				Country.Create("DE"),
				Country.Create("AT"),
				// ...
			});

			confederation1.Equals(confederation2).Should().BeTrue();
		}

		[Test]
		public void EqualShouldReturnTrueForIncorrectlyImplementedCollections()
		{
			ConfederationIncorrectImplementation confederation1 = new ConfederationIncorrectImplementation("European Union", new List<Country>
			{
				Country.Create("FR"),
				Country.Create("DE"),
				Country.Create("AT"),
				// ...
			});

			ConfederationIncorrectImplementation confederation2 = new ConfederationIncorrectImplementation("European Union", new List<Country>
			{
				Country.Create("FR"),
				Country.Create("DE"),
				Country.Create("AT"),
				// ...
			});

			confederation1.Equals(confederation2).Should().BeFalse();
		}
	}
}
