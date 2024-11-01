namespace Fluxera.ValueObject
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for a value object that only contains a single primitive value.
	/// </summary>
	/// <remarks>
	///     The following types are allowed to be used with this class:
	///     Any type that returns <c>true</c>f or <see cref="Type.IsPrimitive" /> and
	///     additionally <see cref="Enum" /> values, <see cref="string" />, <see cref="decimal" />,
	///     <see cref="DateTime" />, <see cref="DateTimeOffset" />, <see cref="TimeSpan" /> and <see cref="Guid" />.
	/// </remarks>
	/// <typeparam name="TValueObject">The type of the value object.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	[PublicAPI]
	[TypeConverter(typeof(PrimitiveValueObjectConverter))]
	public abstract class PrimitiveValueObject<TValueObject, TValue> : 
		IComparable<PrimitiveValueObject<TValueObject, TValue>>, 
		IEquatable<PrimitiveValueObject<TValueObject, TValue>>
		where TValueObject : PrimitiveValueObject<TValueObject, TValue>
		where TValue : IComparable<TValue>, IEquatable<TValue>
	{
		/// <summary>
		///     To ensure hashcode uniqueness, a carefully selected random number multiplier
		///     is used within the calculation.
		/// </summary>
		/// <remarks>
		///     See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// </remarks>
		private const int HashMultiplier = 37;

		static PrimitiveValueObject()
		{
			Type valueType = typeof(TValue);
			bool isPrimitive = valueType.IsPrimitive(true);

			Guard.ThrowIfFalse(isPrimitive, nameof(Value), "The value of a primitive value object must be a primitive, string or enum value.");
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="PrimitiveValueObject{TValueObject, TValue}" /> type.
		/// </summary>
		/// <param name="value"></param>
		protected PrimitiveValueObject(TValue value)
		{
			this.Value = value;
		}

		/// <summary>
		///     Gets or sets the single value of the value object.
		/// </summary>
		public TValue Value { get; private set; }

		/// <summary>
		///		Creates a new instance of the <typeparamref name="TValueObject" />  with the given value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static TValueObject Create(TValue value)
		{
			return (TValueObject)Activator.CreateInstance(typeof(TValueObject), [value]);
		}

		/// <inheritdoc />
		public bool Equals(PrimitiveValueObject<TValueObject, TValue> other)
		{
			return this.Equals(other as object);
		}

		/// <inheritdoc />
		public sealed override bool Equals(object obj)
		{
			if(obj is null)
			{
				return false;
			}

			if(ReferenceEquals(this, obj))
			{
				return true;
			}

			PrimitiveValueObject<TValueObject, TValue> other = obj as PrimitiveValueObject<TValueObject, TValue>;
			return other != null
				&& this.GetType() == other.GetType()
				&& this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
		}

		/// <inheritdoc />
		public int CompareTo(PrimitiveValueObject<TValueObject, TValue> other)
		{
			return (this.Value, other.Value) switch
			{
				(null, null) => 0,
				(null, _)    => -1,
				(_, null)    => 1,
				(_, _)       => this.Value.CompareTo(other.Value)
			};
		}

		/// <summary>
		///     Checks if the given IDs are equal.
		/// </summary>
		public static bool operator ==(PrimitiveValueObject<TValueObject, TValue> left, PrimitiveValueObject<TValueObject, TValue> right)
		{
			if(left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		/// <summary>
		///     Checks if the given IDs are not equal.
		/// </summary>
		public static bool operator !=(PrimitiveValueObject<TValueObject, TValue> left, PrimitiveValueObject<TValueObject, TValue> right)
		{
			return !(left == right);
		}

		/// <summary>
		///     Compares the given primitive value object instances with the lower-than operator.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator <(PrimitiveValueObject<TValueObject, TValue> left, PrimitiveValueObject<TValueObject, TValue> right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>
		///     Compares the given primitive value object instances with the lower-than-equal operator.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator <=(PrimitiveValueObject<TValueObject, TValue> left, PrimitiveValueObject<TValueObject, TValue> right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>
		///     Compares the given primitive value object instances with the greater-than operator.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator >(PrimitiveValueObject<TValueObject, TValue> left, PrimitiveValueObject<TValueObject, TValue> right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>
		///     Compares the given primitive value object instances with the greater-than-equal operator.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator >=(PrimitiveValueObject<TValueObject, TValue> left, PrimitiveValueObject<TValueObject, TValue> right)
		{
			return left.CompareTo(right) >= 0;
		}

		/// <summary>
		///		Converts the value object implicitly to its primitive value.
		/// </summary>
		/// <param name="value"></param>
		public static implicit operator TValue(PrimitiveValueObject<TValueObject, TValue> value)
		{
			return value.Value;
		}

		/// <summary>
		///		Converts the primitive value implicitly to its value object.
		/// </summary>
		/// <param name="value"></param>
		public static implicit operator PrimitiveValueObject<TValueObject, TValue>(TValue value)
		{
			return Create(value);
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
		public override sealed string ToString()
		{
			return this.Value.ToString();
		}

		/// <summary>
		///     Gets all components of the value object that are used for equality. <br />
		///     The default implementation get all properties via reflection. One
		///     can at any time override this behavior with a manual or custom implementation.
		/// </summary>
		/// <returns>The components to use for equality.</returns>
		private IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Value;
		}
	}
}
