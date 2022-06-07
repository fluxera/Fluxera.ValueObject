namespace Fluxera.ValueObject.Collections
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A set with equality based on the content instead on the set's reference,
	///     i.e two different set instances containing the same items will be equal
	///     regardless of their order.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the collection.</typeparam>
	[PublicAPI]
	public sealed class ValueSet<T> : ISet<T>
	{
		/// <summary>
		///     To ensure hashcode uniqueness, a carefully selected random number multiplier
		///     is used within the calculation.
		/// </summary>
		/// <remarks>
		///     See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// </remarks>
		private const int HashMultiplier = 37;

		private readonly ISet<T> hashSet;

		/// <summary>
		///     Initializes a new instance of the <see cref="ValueSet{T}" /> type.
		/// </summary>
		public ValueSet() : this(new HashSet<T>())
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="ValueSet{T}" /> type.
		/// </summary>
		/// <param name="hashSet"></param>
		public ValueSet(ISet<T> hashSet)
		{
			this.hashSet = hashSet;
		}

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator()
		{
			return this.hashSet.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <inheritdoc />
		public void ExceptWith(IEnumerable<T> other)
		{
			this.hashSet.ExceptWith(other);
		}

		/// <inheritdoc />
		public void IntersectWith(IEnumerable<T> other)
		{
			this.hashSet.IntersectWith(other);
		}

		/// <inheritdoc />
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return this.hashSet.IsProperSubsetOf(other);
		}

		/// <inheritdoc />
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return this.hashSet.IsProperSupersetOf(other);
		}

		/// <inheritdoc />
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return this.hashSet.IsSubsetOf(other);
		}

		/// <inheritdoc />
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return this.hashSet.IsSupersetOf(other);
		}

		/// <inheritdoc />
		public bool Overlaps(IEnumerable<T> other)
		{
			return this.hashSet.Overlaps(other);
		}

		/// <inheritdoc />
		public bool SetEquals(IEnumerable<T> other)
		{
			return this.hashSet.SetEquals(other);
		}

		/// <inheritdoc />
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			this.hashSet.SymmetricExceptWith(other);
		}

		/// <inheritdoc />
		public void UnionWith(IEnumerable<T> other)
		{
			this.hashSet.UnionWith(other);
		}

		/// <inheritdoc />
		public bool Add(T item)
		{
			return this.hashSet.Add(item);
		}

		/// <inheritdoc />
		void ICollection<T>.Add(T item)
		{
			if(item != null)
			{
				this.Add(item);
			}
		}

		/// <inheritdoc />
		public void Clear()
		{
			this.hashSet.Clear();
		}

		/// <inheritdoc />
		public bool Contains(T item)
		{
			return this.hashSet.Contains(item);
		}

		/// <inheritdoc />
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.hashSet.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(T item)
		{
			return this.hashSet.Remove(item);
		}

		/// <inheritdoc />
		public int Count => this.hashSet.Count;

		/// <inheritdoc />
		public bool IsReadOnly => this.hashSet.IsReadOnly;

		/// <summary>
		///     Checks two value set instances for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(ValueSet<T> left, ValueSet<T> right)
		{
			if(left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		/// <summary>
		///     Checks two value set instances for non-equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(ValueSet<T> left, ValueSet<T> right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj is null)
			{
				return false;
			}

			if(object.ReferenceEquals(this, obj))
			{
				return true;
			}

			ValueSet<T> other = obj as ValueSet<T>;
			return other != null
				&& this.GetType() == other.GetType()
				&& this.hashSet.SetEquals(other.hashSet);
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

				foreach(object component in this.GetEqualityComponents())
				{
					if(component != null)
					{
						int componentHashCode = component.GetHashCode();
						sortedHashCodes.Add(componentHashCode);
					}
				}

				foreach(int componentHashCode in sortedHashCodes)
				{
					hashCode = hashCode * HashMultiplier ^ componentHashCode;
				}

				return hashCode;
			}
		}

		/// <summary>
		///     Gets all components of the value list that are used for equality.
		/// </summary>
		/// <returns>The components to use for equality.</returns>
		private IEnumerable<object> GetEqualityComponents()
		{
			return this.hashSet.Cast<object>();
		}
	}
}
