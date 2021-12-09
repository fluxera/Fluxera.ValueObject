namespace Fluxera.ValueObject.UnitTests
{
	using System.Collections.Generic;
	using Collections;
	using FluentAssertions;
	using Model;
	using NUnit.Framework;

	[TestFixture]
	public class ValueSetTests
	{
		[Test]
		public void EqualsShouldReturnFalseForSetsContainingSameValueObjectsInDifferentOrder()
		{
			ISet<Country> hashSetOne = new HashSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};
			ISet<Country> hashSetTwo = new HashSet<Country>
			{
				Country.Create("US"), 
				Country.Create("DE")
			};

			ISet<Country> valueSetOne = hashSetOne.AsValueSet();
			ISet<Country> valueSetTwo = hashSetTwo.AsValueSet();

			hashSetOne.Equals(hashSetTwo).Should().BeFalse();
			valueSetOne.Equals(valueSetTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForSetsContainingSameIntegersInDifferentOrder()
		{
			ISet<int> hashSetOne = new HashSet<int> { 1, 2, 3, 4 };
			ISet<int> hashSetTwo = new HashSet<int> { 4, 3, 2, 1 };

			ISet<int> valueSetOne = hashSetOne.AsValueSet();
			ISet<int> valueSetTwo = hashSetTwo.AsValueSet();

			hashSetOne.Equals(hashSetTwo).Should().BeFalse();
			valueSetOne.Equals(valueSetTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForSetsContainingSameIntegersInSameOrder()
		{
			ISet<int> hashSetOne = new HashSet<int> { 1, 2, 3, 4 };
			ISet<int> hashSetTwo = new HashSet<int> { 1, 2, 3, 4 };

			ISet<int> valueSetOne = hashSetOne.AsValueSet();
			ISet<int> valueSetTwo = hashSetTwo.AsValueSet();

			hashSetOne.Equals(hashSetTwo).Should().BeFalse();
			valueSetOne.Equals(valueSetTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForSetsContainingSameObjectsInDifferentOrder()
		{
			object first = new object();
			object second = new object();

			ISet<object> hashSetOne = new HashSet<object> { first, second };
			ISet<object> hashSetTwo = new HashSet<object> { second, first };

			ISet<object> valueSetOne = hashSetOne.AsValueSet();
			ISet<object> valueSetTwo = hashSetTwo.AsValueSet();

			hashSetOne.Equals(hashSetTwo).Should().BeFalse();
			valueSetOne.Equals(valueSetTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForSetsContainingSameObjectsInSameOrder()
		{
			object first = new object();
			object second = new object();

			ISet<object> hashSetOne = new HashSet<object> { first, second };
			ISet<object> hashSetTwo = new HashSet<object> { first, second };

			ISet<object> valueSetOne = hashSetOne.AsValueSet();
			ISet<object> valueSetTwo = hashSetTwo.AsValueSet();

			hashSetOne.Equals(hashSetTwo).Should().BeFalse();
			valueSetOne.Equals(valueSetTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForSetsContainingSameStringsInDifferentOrder()
		{
			ISet<string> hashSetOne = new HashSet<string> { "John", "Paul", "George", "Ringo" };
			ISet<string> hashSetTwo = new HashSet<string> { "John", "Paul", "Ringo", "George" };

			ISet<string> valueSetOne = hashSetOne.AsValueSet();
			ISet<string> valueSetTwo = hashSetTwo.AsValueSet();

			hashSetOne.Equals(hashSetTwo).Should().BeFalse();
			valueSetOne.Equals(valueSetTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForSetsContainingSameStringsInSameOrder()
		{
			ISet<string> hashSetOne = new HashSet<string> { "John", "Paul", "George", "Ringo" };
			ISet<string> hashSetTwo = new HashSet<string> { "John", "Paul", "George", "Ringo" };

			ISet<string> valueSetOne = hashSetOne.AsValueSet();
			ISet<string> valueSetTwo = hashSetTwo.AsValueSet();

			hashSetOne.Equals(hashSetTwo).Should().BeFalse();
			valueSetOne.Equals(valueSetTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForSetsContainingSameValueObjectsInSameOrder()
		{
			ISet<Country> hashSetOne = new HashSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};
			ISet<Country> hashSetTwo = new HashSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};

			ISet<Country> valueSetOne = hashSetOne.AsValueSet();
			ISet<Country> valueSetTwo = hashSetTwo.AsValueSet();

			hashSetOne.Equals(hashSetTwo).Should().BeFalse();
			valueSetOne.Equals(valueSetTwo).Should().BeTrue();
		}

		[Test]
		public void ShouldAcceptValuesInCtor()
		{
			ISet<int> hashSet = new HashSet<int> { 1, 2, 3, 4 };
			ISet<int> valueSet = hashSet.AsValueSet();

			valueSet.Should().BeOfType<ValueSet<int>>();
			valueSet.Should().ContainInOrder(1, 2, 3, 4);
		}

		[Test]
		public void ShouldProperlyExposeContains()
		{
			ISet<Country> valueSet = new ValueSet<Country>
			{
				Country.Create("DE"),
				Country.Create("US")
			};

			valueSet.Contains(Country.Create("US")).Should().BeTrue();
		}

		[Test]
		public void ShouldProperlyExposeCopyTo()
		{
			ISet<Country> valueSet = new ValueSet<Country>
			{
				Country.Create("DE"),
				Country.Create("US")
			};

			Country[] array = new Country[5];
			valueSet.CopyTo(array, 2);

			array.Should().ContainInOrder(null, null, Country.Create("DE"), Country.Create("US"), null);
		}

		[Test]
		public void ShouldProperlyExposeCount()
		{
			ISet<Country> valueSet = new ValueSet<Country>
			{
				Country.Create("DE"),
				Country.Create("US")
			};
			valueSet.Count.Should().Be(2);

			valueSet.Add(Country.Create("AT"));
			valueSet.Count.Should().Be(3);
		}

		[Test]
		public void ShouldProperlyExposeIsProperSubsetOf()
		{
			ISet<Country> hashSet = new HashSet<Country>
			{
				Country.Create("DE"),
				Country.Create("US")
			};
			ISet<Country> valueSet = hashSet.AsValueSet();

			bool expected = hashSet.IsProperSubsetOf(new[] { Country.Create("DE") });
			bool result = valueSet.IsProperSubsetOf(new[] { Country.Create("DE") });

			result.Should().Be(expected);
		}

		[Test]
		public void ShouldProperlyExposeIsProperSupersetOf()
		{
			ISet<Country> hashSet = new HashSet<Country>
			{
				Country.Create("DE"),
				Country.Create("US")
			};
			ISet<Country> valueSet = hashSet.AsValueSet();

			bool expected = hashSet.IsProperSupersetOf(new[] { Country.Create("DE") });
			bool result = valueSet.IsProperSupersetOf(new[] { Country.Create("DE") });

			result.Should().Be(expected);
		}

		[Test]
		public void ShouldProperlyExposeIsReadOnly()
		{
			ISet<Country> hashSet = new HashSet<Country>
			{
				Country.Create("DE"),
				Country.Create("US")
			};
			ISet<Country> valueSet = hashSet.AsValueSet();

			valueSet.IsReadOnly.Should().Be(hashSet.IsReadOnly);
		}

		[Test]
		public void ShouldProperlyExposeIsSubsetOf()
		{
			ISet<Country> hashSet = new HashSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};
			ISet<Country> valueSet = hashSet.AsValueSet();

			bool expected = hashSet.IsSubsetOf(new[] { Country.Create("DE") });
			bool result = valueSet.IsSubsetOf(new[] { Country.Create("DE") });

			result.Should().Be(expected);
		}

		[Test]
		public void ShouldProperlyExposeIsSupersetOf()
		{
			ISet<Country> hashSet = new HashSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};
			ISet<Country> valueSet = hashSet.AsValueSet();

			bool expected = hashSet.IsSupersetOf(new[] { Country.Create("DE") });
			bool result = valueSet.IsSupersetOf(new[] { Country.Create("DE") });

			result.Should().Be(expected);
		}

		[Test]
		public void ShouldProperlyExposeOverlaps()
		{
			ISet<Country> hashSet = new HashSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};
			ISet<Country> valueSet = hashSet.AsValueSet();

			bool expected = hashSet.Overlaps(new[] { Country.Create("DE") });
			bool result = valueSet.Overlaps(new[] { Country.Create("DE") });

			result.Should().Be(expected);
		}

		[Test]
		public void ShouldProperlyExposeSetEquals()
		{
			ISet<Country> hashSet = new HashSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};
			ISet<Country> valueSet = hashSet.AsValueSet();

			bool expected = hashSet.SetEquals(new[] { Country.Create("DE") });
			bool result = valueSet.SetEquals(new[] { Country.Create("DE") });

			result.Should().Be(expected);
		}

		[Test]
		public void ShouldProvideDifferentHashCodeForTwoDifferentSets()
		{
			ISet<Country> valueSetOne = new ValueSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};
			ISet<Country> valueSetTwo = new ValueSet<Country>
			{
				Country.Create("AT"),
				Country.Create("US")
			};

			valueSetOne.GetHashCode().Should().NotBe(valueSetTwo.GetHashCode());
		}

		[Test]
		public void ShouldProvideSameHashCodeForTwoDifferentSets()
		{
			ISet<Country> valueSetOne = new ValueSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};
			ISet<Country> valueSetTwo = new ValueSet<Country>
			{
				Country.Create("DE"),
				Country.Create("US")
			};

			valueSetOne.GetHashCode().Should().Be(valueSetTwo.GetHashCode());
		}

		[Test]
		public void ShouldProvideSameHashCodeForTwoDifferentSetsWithSameValesInDifferentOrder()
		{
			ISet<Country> valueSetOne = new ValueSet<Country>
			{
				Country.Create("DE"), 
				Country.Create("US")
			};
			ISet<Country> valueSetTwo = new ValueSet<Country>
			{
				Country.Create("US"), 
				Country.Create("DE")
			};

			valueSetOne.GetHashCode().Should().Be(valueSetTwo.GetHashCode());
		}
	}
}
