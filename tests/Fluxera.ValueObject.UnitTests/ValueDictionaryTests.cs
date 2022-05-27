namespace Fluxera.ValueObject.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.ValueObject.Collections;
	using Fluxera.ValueObject.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ValueDictionaryTests
	{
		[Test]
		public void EqualsShouldReturnFalseForDictionariesContainingDifferentValues()
		{
			IDictionary<int, string> dictOne = new Dictionary<int, string>
			{
				{ 1, "one" },
				{ 3, "three" },
			};
			IDictionary<int, string> dictTwo = new Dictionary<int, string>
			{
				{ 1, "one" },
				{ 3, "three" },
				{ 4, "four" },
			};

			IDictionary<int, string> valueDictOne = dictOne.AsValueDictionary();
			IDictionary<int, string> valueDictTwo = dictTwo.AsValueDictionary();

			dictOne.Equals(dictTwo).Should().BeFalse();
			valueDictOne.Equals(valueDictTwo).Should().BeFalse();
		}

		[Test]
		public void EqualsShouldReturnFalseForDictionaryAndSet()
		{
			IDictionary<int, string> valueDict = new ValueDictionary<int, string>
			{
				{ 1, "one" },
				{ 4, "four" },
				{ 3, "three" },
			};
			ISet<KeyValuePair<int, string>> valueSet = new ValueSet<KeyValuePair<int, string>>
			{
				new KeyValuePair<int, string>(1, "one"),
				new KeyValuePair<int, string>(4, "four"),
				new KeyValuePair<int, string>(3, "three")
			};

			valueDict.Equals(valueSet).Should().BeFalse();
		}

		[Test]
		public void EqualsShouldReturnFalseForDifferentDictionariesWithDifferentElements()
		{
			IDictionary<int, string> dictOne = new Dictionary<int, string>
			{
				{ 1, "one" },
				{ 4, "four" },
				{ 3, "three" }
			};
			IDictionary<int, string> dictTwo = new Dictionary<int, string>
			{
				{ 1, "one" },
				{ 7, "seven" },
				{ 4, "four" }
			};

			IDictionary<int, string> valueDictOne = dictOne.AsValueDictionary();
			IDictionary<int, string> valueDictTwo = dictTwo.AsValueDictionary();

			dictOne.Equals(dictTwo).Should().BeFalse();
			valueDictOne.Equals(valueDictTwo).Should().BeFalse();
		}

		[Test]
		public void EqualsShouldReturnFalseForDifferentDictionariesWithDifferentIntElements()
		{
			IDictionary<int, int> dictOne = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 4, 4 },
				{ 3, 3 }
			};
			IDictionary<int, int> dictTwo = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 4, 7 },
				{ 3, 3 }
			};

			IDictionary<int, int> valueDictOne = dictOne.AsValueDictionary();
			IDictionary<int, int> valueDictTwo = dictTwo.AsValueDictionary();

			dictOne.Equals(dictTwo).Should().BeFalse();
			valueDictOne.Equals(valueDictTwo).Should().BeFalse();
		}

		[Test]
		public void EqualsShouldReturnFalseForDifferentDictionariesWithDifferentValueObjectElements()
		{
			IDictionary<int, Country> dictOne = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};
			IDictionary<int, Country> dictTwo = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("AT") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDictOne = dictOne.AsValueDictionary();
			IDictionary<int, Country> valueDictTwo = dictTwo.AsValueDictionary();

			dictOne.Equals(dictTwo).Should().BeFalse();
			valueDictOne.Equals(valueDictTwo).Should().BeFalse();
		}

		[Test]
		public void EqualsShouldReturnTrueForDifferentDictionariesWithSameElementsInDifferentOrder()
		{
			IDictionary<int, string> dictOne = new Dictionary<int, string>
			{
				{ 1, "one" },
				{ 4, "four" },
				{ 3, "three" }
			};
			IDictionary<int, string> dictTwo = new Dictionary<int, string>
			{
				{ 1, "one" },
				{ 3, "three" },
				{ 4, "four" }
			};

			IDictionary<int, string> valueDictOne = dictOne.AsValueDictionary();
			IDictionary<int, string> valueDictTwo = dictTwo.AsValueDictionary();

			dictOne.Equals(dictTwo).Should().BeFalse();
			valueDictOne.Equals(valueDictTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForDifferentDictionariesWithSameElementsInSameOrder()
		{
			IDictionary<int, string> dictOne = new Dictionary<int, string>
			{
				{ 1, "one" },
				{ 4, "four" },
				{ 3, "three" },
			};
			IDictionary<int, string> dictTwo = new Dictionary<int, string>
			{
				{ 1, "one" },
				{ 4, "four" },
				{ 3, "three" },
			};

			IDictionary<int, string> valueDictOne = dictOne.AsValueDictionary();
			IDictionary<int, string> valueDictTwo = dictTwo.AsValueDictionary();

			dictOne.Equals(dictTwo).Should().BeFalse();
			valueDictOne.Equals(valueDictTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForDifferentDictionariesWithSameIntElements()
		{
			IDictionary<int, int> dictOne = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 4, 4 },
				{ 3, 3 }
			};
			IDictionary<int, int> dictTwo = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 4, 4 },
				{ 3, 3 }
			};

			IDictionary<int, int> valueDictOne = dictOne.AsValueDictionary();
			IDictionary<int, int> valueDictTwo = dictTwo.AsValueDictionary();

			dictOne.Equals(dictTwo).Should().BeFalse();
			valueDictOne.Equals(valueDictTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForDifferentDictionariesWithSameValueObjectElementsWithDifferentOrder()
		{
			IDictionary<int, Country> dictOne = new Dictionary<int, Country>
			{
				{ 3, Country.Create("UK") },
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
			};
			IDictionary<int, Country> dictTwo = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDictOne = dictOne.AsValueDictionary();
			IDictionary<int, Country> valueDictTwo = dictTwo.AsValueDictionary();

			dictOne.Equals(dictTwo).Should().BeFalse();
			valueDictOne.Equals(valueDictTwo).Should().BeTrue();
		}

		[Test]
		public void EqualsShouldReturnTrueForDifferentDictionariesWithSameValueObjectElementsWithSameOrder()
		{
			IDictionary<int, Country> dictOne = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};
			IDictionary<int, Country> dictTwo = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDictOne = dictOne.AsValueDictionary();
			IDictionary<int, Country> valueDictTwo = dictTwo.AsValueDictionary();

			dictOne.Equals(dictTwo).Should().BeFalse();
			valueDictOne.Equals(valueDictTwo).Should().BeTrue();
		}

		[Test]
		public void ShouldAcceptValuesInCtor()
		{
			IDictionary<int, int> dict = new Dictionary<int, int>
			{
				{ 1, 111 },
				{ 2, 222 },
				{ 3, 333 },
				{ 4, 444 },
			};
			IDictionary<int, int> valueDict = dict.AsValueDictionary();

			valueDict.Should().BeOfType<ValueDictionary<int, int>>();
			valueDict.Keys.Should().ContainInOrder(1, 2, 3, 4);
			valueDict.Values.Should().ContainInOrder(111, 222, 333, 444);
		}

		[Test]
		public void ShouldExposeContains()
		{
			IDictionary<int, Country> dict = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDict = dict.AsValueDictionary();

			valueDict.Contains(new KeyValuePair<int, Country>(4, Country.Create("US"))).Should().BeTrue();
			valueDict.Contains(new KeyValuePair<int, Country>(5, Country.Create("US"))).Should().BeFalse();
		}

		[Test]
		public void ShouldExposeContainsKey()
		{
			IDictionary<int, Country> dict = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDict = dict.AsValueDictionary();

			valueDict.ContainsKey(4).Should().BeTrue();
			valueDict.ContainsKey(7).Should().BeFalse();
		}

		[Test]
		public void ShouldExposeCopyTo()
		{
			IDictionary<int, Country> dict = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDict = dict.AsValueDictionary();

			KeyValuePair<int, Country>[] array = new KeyValuePair<int, Country>[5];
			valueDict.CopyTo(array, 2);

			array.Should().ContainInOrder(
				new KeyValuePair<int, Country>(0, null),
				new KeyValuePair<int, Country>(0, null),
				new KeyValuePair<int, Country>(1, Country.Create("DE")),
				new KeyValuePair<int, Country>(3, Country.Create("UK")),
				new KeyValuePair<int, Country>(0, null));
		}

		[Test]
		public void ShouldExposeCount()
		{
			IDictionary<int, Country> dict = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDict = dict.AsValueDictionary();

			valueDict.Count.Should().Be(dict.Count);
		}

		[Test]
		public void ShouldExposeIndexer()
		{
			IDictionary<int, Country> dict = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDict = dict.AsValueDictionary();

			valueDict[1].Should().Be(Country.Create("DE"));

			valueDict[42] = Country.Create("AT");
			valueDict[42].Should().Be(Country.Create("AT"));
		}

		[Test]
		public void ShouldExposeIsReadOnly()
		{
			IDictionary<int, Country> dict = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDict = dict.AsValueDictionary();

			valueDict.IsReadOnly.Should().Be(dict.IsReadOnly);
		}

		[Test]
		public void ShouldExposeKeys()
		{
			IDictionary<int, Country> dict = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDict = dict.AsValueDictionary();

			valueDict.Keys.Should().ContainInOrder(1, 4, 3);
		}

		[Test]
		public void ShouldExposeTryGetValue()
		{
			IDictionary<int, Country> dict = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDict = dict.AsValueDictionary();

			valueDict.TryGetValue(3, out Country result);
			result.Should().NotBeNull();
			result.Should().Be(Country.Create("UK"));

			valueDict.TryGetValue(23, out Country nullResult);
			nullResult.Should().BeNull();
		}

		[Test]
		public void ShouldExposeValues()
		{
			IDictionary<int, Country> dict = new Dictionary<int, Country>
			{
				{ 1, Country.Create("DE") },
				{ 4, Country.Create("US") },
				{ 3, Country.Create("UK") },
			};

			IDictionary<int, Country> valueDict = dict.AsValueDictionary();

			valueDict.Values.Should().ContainInOrder(Country.Create("DE"), Country.Create("US"), Country.Create("UK"));
		}
	}
}
