namespace Fluxera.ValueObject.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using Model;
	using NUnit.Framework;

	[TestFixture]
	public class EqualsTests
	{
		[Test]		
		[TestCaseSource(nameof(TestData))]
		public void EqualShouldReturnExpectedValue(object first, object second, bool expected)
		{
			bool result = first.Equals(second);
			result.Should().Be(expected);
		}

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
	}
}
