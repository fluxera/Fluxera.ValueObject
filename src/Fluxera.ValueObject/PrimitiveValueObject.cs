namespace Fluxera.ValueObject
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for a value object that only contains a single primitive value.
	/// </summary>
	/// <typeparam name="TValueObject">The type of the value object.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	[PublicAPI]
	public abstract class PrimitiveValueObject<TValueObject, TValue> : ValueObject<TValueObject>
		where TValueObject : ValueObject<TValueObject>
	{
		/// <summary>
		///     Gets or sets the single value of the value object.
		/// </summary>
		public TValue Value { get; set; }

		/// <inheritdoc />
		protected sealed override IEnumerable<object?> GetEqualityComponents()
		{
			yield return this.Value;
		}
	}
}
