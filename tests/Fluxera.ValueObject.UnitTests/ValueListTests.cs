namespace Fluxera.ValueObject.UnitTests
{
	using System.Collections.Generic;
	using Collections;
	using FluentAssertions;
	using Model;
	using NUnit.Framework;

	[TestFixture]
	public class ValueListTests
	{
		[Test]
		public void ShouldAcceptValuesInCtor()
		{
			IList<int> list = new List<int> { 1, 2, 3, 4 };
			IList<int> valueList = list.AsValueList();

			valueList.Should().BeOfType<ValueList<int>>();
			valueList.Should().ContainInOrder(1, 2, 3, 4);
		}

		[Test]
		public void EqualsShouldReturnTrueForListsContainingSameIntegersInSameOrder()
		{
			IList<int> listOne = new List<int> { 1, 2, 3, 4 };
			IList<int> listTwo = new List<int> { 1, 2, 3, 4 };

			IList<int> valueListOne = listOne.AsValueList();
			IList<int> valueListTwo = listTwo.AsValueList();

			listOne.Equals(listTwo).Should().BeFalse();
			valueListOne.Equals(valueListTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnFalseForListsContainingSameIntegersInDifferentOrder()
		{
			IList<int> listOne = new List<int> { 1, 2, 3, 4 };
			IList<int> listTwo = new List<int> { 4, 3, 2, 1 };

			IList<int> valueListOne = listOne.AsValueList();
			IList<int> valueListTwo = listTwo.AsValueList();

			listOne.Equals(listTwo).Should().BeFalse();
			valueListOne.Equals(valueListTwo).Should().BeFalse();
		}

		[Test]
		public void EqualsShouldReturnTrueForListsContainingSameValueObjectsInSameOrder()
		{
			IList<Country> listOne = new List<Country> { Country.Create("DE"), Country.Create("US") };
			IList<Country> listTwo = new List<Country> { Country.Create("DE"), Country.Create("US") };

			IList<Country> valueListOne = listOne.AsValueList();
			IList<Country> valueListTwo = listTwo.AsValueList();

			listOne.Equals(listTwo).Should().BeFalse();
			valueListOne.Equals(valueListTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnFalseForListsContainingSameValueObjectsInDifferentOrder()
		{
			IList<Country> listOne = new List<Country> { Country.Create("DE"), Country.Create("US") };
			IList<Country> listTwo = new List<Country> { Country.Create("US"), Country.Create("DE") };

			IList<Country> valueListOne = listOne.AsValueList();
			IList<Country> valueListTwo = listTwo.AsValueList();

			listOne.Equals(listTwo).Should().BeFalse();
			valueListOne.Equals(valueListTwo).Should().BeFalse();
		}

		[Test]
		public void EqualsShouldReturnTrueForListsContainingSameObjectsInSameOrder()
		{
			object first = new object();
			object second = new object();

			IList<object> listOne = new List<object> { first, second };
			IList<object> listTwo = new List<object> { first, second  };

			IList<object> valueListOne = listOne.AsValueList();
			IList<object> valueListTwo = listTwo.AsValueList();

			listOne.Equals(listTwo).Should().BeFalse();
			valueListOne.Equals(valueListTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnFalseForListsContainingSameObjectsInDifferentOrder()
		{
			object first = new object();
			object second = new object();

			IList<object> listOne = new List<object> { second, first };
			IList<object> listTwo = new List<object> { first, second  };

			IList<object> valueListOne = listOne.AsValueList();
			IList<object> valueListTwo = listTwo.AsValueList();

			listOne.Equals(listTwo).Should().BeFalse();
			valueListOne.Equals(valueListTwo).Should().BeFalse();
		}

		[Test]
		public void ShouldProperlyExposeCount()
		{
			IList<Country> list = new ValueList<Country> { Country.Create("DE"), Country.Create("US") };
			list.Count.Should().Be(2);

			list.Add(Country.Create("AT"));
			list.Count.Should().Be(3);
		}

		[Test]
		public void ShouldProperlyExposeContains()
		{
			IList<Country> list = new ValueList<Country> { Country.Create("DE"), Country.Create("US") };

			list.Contains(Country.Create("DE")).Should().BeTrue();
			list.Contains(Country.Create("US")).Should().BeTrue();
		}

		[Test]
		public void ShouldProperlyExposeCopyTo()
		{
			IList<Country> list = new ValueList<Country> { Country.Create("DE"), Country.Create("US") };
			Country[] array = new Country[5];
			list.CopyTo(array, 2);

			array.Should().ContainInOrder(null, null, Country.Create("DE"), Country.Create("US"), null);
		}

		[Test]
		public void ShouldProperlyExposeEnumerable()
		{
			IList<Country> list = new ValueList<Country> { Country.Create("DE"), Country.Create("US") };

			foreach(Country country in list)
			{
				(country == Country.Create("DE") || country == Country.Create("US")).Should().BeTrue();
			}
		}

		[Test]
		public void ShouldProperlyExposeIndexer()
		{
			IList<Country> list = new ValueList<Country> { Country.Create("DE"), Country.Create("US") };

			list[0].Should().Be(Country.Create("DE"));
			list[1].Should().Be(Country.Create("US"));
		}

		[Test]
		public void ShouldProperlyExposeIndexOf()
		{
			IList<Country> list = new ValueList<Country> { Country.Create("DE"), Country.Create("US") };

			list.IndexOf(Country.Create("DE")).Should().Be(0);
			list.IndexOf(Country.Create("US")).Should().Be(1);
		}

		[Test]
		public void ShouldProperlyExposeIsReadOnly()
		{
			IList<Country> originalList = new List<Country> { Country.Create("DE"), Country.Create("US") };
			IList<Country> valueList = originalList.AsValueList();

			ICollection<Country> original = originalList;
			valueList.IsReadOnly.Should().Be(original.IsReadOnly);
		}
	}		
}
