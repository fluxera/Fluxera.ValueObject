namespace Fluxera.ValueObject
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
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
	public abstract class PrimitiveValueObject<TValueObject, TValue> : ValueObject<TValueObject>
		where TValueObject : ValueObject<TValueObject>
	{
		static PrimitiveValueObject()
		{
			Type valueType = typeof(TValue);
			bool isPrimitive = valueType.IsPrimitive(true, true);

			Guard.Against.False(isPrimitive, nameof(Value), "The value of a primitive value object must be a primitive, string or enum value.");
		}

		/// <summary>
		///     Gets or sets the single value of the value object.
		/// </summary>
		public TValue? Value { get; set; }

		/// <inheritdoc />
		protected sealed override IEnumerable<object?> GetEqualityComponents()
		{
			yield return this.Value;
		}
	}
}
