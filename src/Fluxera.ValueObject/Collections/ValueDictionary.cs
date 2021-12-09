namespace Fluxera.ValueObject.Collections
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A dictionary with equality based on the content instead on the dictionary's reference,
	///     i.e. two different dictionary instances containing the same items will be equal.
	/// </summary>
	/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
	[PublicAPI]
	public sealed class ValueDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		/// <summary>
		///     To ensure hashcode uniqueness, a carefully selected random number multiplier
		///     is used within the calculation.
		/// </summary>
		/// <remarks>
		///     See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// </remarks>
		private const int HashMultiplier = 37;

		private readonly IDictionary<TKey, TValue> dictionary;

		public ValueDictionary() : this(new Dictionary<TKey, TValue>())
		{
		}

		public ValueDictionary(IDictionary<TKey, TValue> dictionary)
		{
			this.dictionary = dictionary;
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <inheritdoc />
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.dictionary.Add(item);
		}

		/// <inheritdoc />
		public void Clear()
		{
			this.dictionary.Clear();
		}

		/// <inheritdoc />
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.dictionary.Contains(item);
		}

		/// <inheritdoc />
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.dictionary.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return this.dictionary.Remove(item);
		}

		/// <inheritdoc />
		public int Count => this.dictionary.Count;

		/// <inheritdoc />
		public bool IsReadOnly => this.dictionary.IsReadOnly;

		/// <inheritdoc />
		public void Add(TKey key, TValue value)
		{
			this.dictionary.Add(key, value);
		}

		/// <inheritdoc />
		public bool ContainsKey(TKey key)
		{
			return this.dictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		public bool Remove(TKey key)
		{
			return this.dictionary.Remove(key);
		}

		/// <inheritdoc />
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		public TValue this[TKey key]
		{
			get => this.dictionary[key];
			set => this.dictionary[key] = value;
		}

		/// <inheritdoc />
		public ICollection<TKey> Keys => this.dictionary.Keys;

		/// <inheritdoc />
		public ICollection<TValue> Values => this.dictionary.Values;

		public static bool operator ==(ValueDictionary<TKey, TValue>? left, ValueDictionary<TKey, TValue>? right)
		{
			if(left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		public static bool operator !=(ValueDictionary<TKey, TValue>? left, ValueDictionary<TKey, TValue>? right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if(obj is null)
			{
				return false;
			}

			if(object.ReferenceEquals(this, obj))
			{
				return true;
			}

			if(obj is not ValueDictionary<TKey, TValue> other)
			{
				return false;
			}

			IDictionary<TKey, TValue> longerDict;
			IDictionary<TKey, TValue> shorterDict;
			if(this.dictionary.Count >= other.Count)
			{
				longerDict = this.dictionary;
				shorterDict = other.dictionary;
			}
			else
			{
				longerDict = other.dictionary;
				shorterDict = this.dictionary;
			}

			return (this.GetType() == other.GetType())
				&& !longerDict.Except(shorterDict).Any();
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				// It is possible for two objects to return the same hash code based on
				// identically valued properties, even if they are of different types,
				// so we include the value object type in the hash calculation
				int hashCode = this.GetType().GetHashCode();

				// Two instances with the same elements added in different order must return
				// the same hashcode. We do this by computing and sorting the hashcode of all
				// elements, so wo have always the same order.
				ISet<int> sortedHashCodes = new SortedSet<int>();

				foreach(object? component in this.GetEqualityComponents())
				{
					if(component != null)
					{
						int componentHashCode = component.GetHashCode();
						sortedHashCodes.Add(componentHashCode);
					}
				}

				foreach(int componentHashCode in sortedHashCodes)
				{
					hashCode = (hashCode * HashMultiplier) ^ componentHashCode;
				}

				return hashCode;
			}
		}

		/// <summary>
		///     Gets all components of the value list that are used for equality.
		/// </summary>
		/// <returns>The components to use for equality.</returns>
		private IEnumerable<object?> GetEqualityComponents()
		{
			foreach(KeyValuePair<TKey, TValue> keyValuePair in this.dictionary)
			{
				yield return keyValuePair;
			}
		}
	}
}
