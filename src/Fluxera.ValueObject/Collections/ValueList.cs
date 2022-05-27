namespace Fluxera.ValueObject.Collections
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A list with equality based on the content instead on the list's
	///     reference, i.e. two different list instances containing the same
	///     items in the same order will be equal.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the collection.</typeparam>
	[PublicAPI]
	public sealed class ValueList<T> : IList<T>
	{
		/// <summary>
		///     To ensure hashcode uniqueness, a carefully selected random number multiplier
		///     is used within the calculation.
		/// </summary>
		/// <remarks>
		///     See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// </remarks>
		private const int HashMultiplier = 37;

		private readonly IList<T> list;

		/// <summary>
		///     Initializes a new instance of the <see cref="ValueList{T}" /> type.
		/// </summary>
		public ValueList() : this(new List<T>())
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="ValueList{T}" /> type.
		/// </summary>
		/// <param name="list"></param>
		public ValueList(IList<T> list)
		{
			this.list = list;
		}

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <inheritdoc />
		public void Add(T item)
		{
			this.list.Add(item);
		}

		/// <inheritdoc />
		public void Clear()
		{
			this.list.Clear();
		}

		/// <inheritdoc />
		public bool Contains(T item)
		{
			return this.list.Contains(item);
		}

		/// <inheritdoc />
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(T item)
		{
			return this.list.Remove(item);
		}

		/// <inheritdoc />
		public int Count => this.list.Count;

		/// <inheritdoc />
		public bool IsReadOnly => this.list.IsReadOnly;

		/// <inheritdoc />
		public int IndexOf(T item)
		{
			return this.list.IndexOf(item);
		}

		/// <inheritdoc />
		public void Insert(int index, T item)
		{
			this.list.Insert(index, item);
		}

		/// <inheritdoc />
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		/// <inheritdoc />
		public T this[int index]
		{
			get => this.list[index];
			set => this.list[index] = value;
		}

		/// <summary>
		///     Checks two value list instances for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(ValueList<T> left, ValueList<T> right)
		{
			if(left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		/// <summary>
		///     Checks two value list instances for non-quality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(ValueList<T> left, ValueList<T> right)
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

			ValueList<T> other = obj as ValueList<T>;
			return other != null
				&& this.GetType() == other.GetType()
				&& this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
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

				foreach(object component in this.GetEqualityComponents())
				{
					if(component != null)
					{
						hashCode = hashCode * HashMultiplier ^ component.GetHashCode();
					}
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
			return this.list.Cast<object>();
		}
	}
}
