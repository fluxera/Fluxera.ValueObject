namespace Fluxera.ValueObject.EntityFrameworkCore
{
	using System;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class PrimitiveValueObjectConverter<TValueObject, TValue> : ValueConverter<TValueObject, TValue>
		where TValueObject : PrimitiveValueObject<TValueObject, TValue>
		where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="PrimitiveValueObjectConverter{TValueObject,TValue}" /> type.
		/// </summary>
		public PrimitiveValueObjectConverter()
			: base(valueObject => Serialize(valueObject), value => Deserialize(value))
		{
		}

		private static TValue Serialize(TValueObject valueObject)
		{
			TValue value = valueObject.Value;
			return value;
		}

		private static TValueObject Deserialize(TValue value)
		{
			if(value is null)
			{
				return null;
			}

			object instance = Activator.CreateInstance(typeof(TValueObject), BindingFlags.Public | BindingFlags.Instance, null, new object[] { value }, null);
			return (TValueObject)instance;
		}
	}
}
