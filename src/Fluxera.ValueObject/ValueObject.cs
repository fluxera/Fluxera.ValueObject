namespace Fluxera.ValueObject
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for any value object.
	/// </summary>
	/// <typeparam name="TValueObject">The type of the value object.</typeparam>
	[PublicAPI]
	public abstract class ValueObject<TValueObject> : IEquatable<TValueObject>
		where TValueObject : ValueObject<TValueObject>
	{
		/// <summary>
		///     To ensure hashcode uniqueness, a carefully selected random number multiplier
		///     is used within the calculation.
		/// </summary>
		/// <remarks>
		///     See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// </remarks>
		private const int HashMultiplier = 37;

		/// <inheritdoc />
		public bool Equals(TValueObject other)
		{
			return this.Equals(other as object);
		}

		/// <summary>
		///     Checks if the given value objects are equal.
		/// </summary>
		public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
		{
			if(left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		/// <summary>
		///     Checks if the given value objects are not equal.
		/// </summary>
		public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public sealed override bool Equals(object obj)
		{
			if(obj is null)
			{
				return false;
			}

			if(object.ReferenceEquals(this, obj))
			{
				return true;
			}

			ValueObject<TValueObject> other = obj as ValueObject<TValueObject>;
			return other != null
				&& this.GetType() == other.GetType()
				&& this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
		}

		/// <inheritdoc />
		public sealed override int GetHashCode()
		{
			unchecked
			{
				// It is possible for two objects to return the same hash code based on
				// identically valued properties, even if they are of different types,
				// so we include the value object type in the hash calculation.
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

		/// <inheritdoc />
		public sealed override string ToString()
		{
			using(IEnumerator<object> enumerator = this.GetEqualityComponents().GetEnumerator())
			{
				if(!enumerator.MoveNext())
				{
					return $"{typeof(TValueObject).Name} {{}}";
				}

				StringBuilder builder = new StringBuilder($"{typeof(TValueObject).Name} {{ ");
				builder.Append(enumerator.Current);
				while(enumerator.MoveNext())
				{
					builder.Append(" ,").Append(enumerator.Current);
				}

				builder.Append(" }");

				return builder.ToString();
			}
		}

		/// <summary>
		///     Gets all components of the value object that are used for equality. <br />
		///     The default implementation get all properties via reflection. One
		///     can at any time override this behavior with a manual or custom implementation.
		/// </summary>
		/// <returns>The components to use for equality.</returns>
		protected virtual IEnumerable<object> GetEqualityComponents()
		{
			PropertyAccessor[] propertyAccessors = PropertyAccessor.GetPropertyAccessors(this.GetType());
			foreach(PropertyAccessor accessor in propertyAccessors)
			{
				object value = accessor.Invoke(this);
				yield return value;
			}
		}
	}
}
