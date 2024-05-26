namespace Fluxera.ValueObject
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
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
	public abstract class PrimitiveValueObject<TValueObject, TValue> : ValueObject<TValueObject>, IComparable<TValueObject>
		where TValueObject : PrimitiveValueObject<TValueObject, TValue>
		where TValue : IComparable
	{
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

		/// <inheritdoc />
		public int CompareTo(TValueObject other)
		{
			return (this.Value, other.Value) switch
			{
				(null, null) => 0,
				(null, _) => -1,
				(_, null) => 1,
				(_, _) => this.Value.CompareTo(other.Value)
			};
		}

		/// <inheritdoc />
		protected sealed override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Value;
		}

		/// <inheritdoc />
		public override sealed string ToString()
		{
			return this.Value.ToString();
		}
	}
}
