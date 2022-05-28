namespace Fluxera.ValueObject.LiteDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Guards;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="BsonMapper" /> type.
	/// </summary>
	[PublicAPI]
	public static class BsonMapperExtensions
	{
		public static BsonMapper UsePrimitiveValueObject(this BsonMapper mapper)
		{
			Guard.Against.Null(mapper);

			IEnumerable<Type> primitiveValueObjectTypes = AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(x => x.GetTypes())
				.Where(x => x.IsPrimitiveValueObject());

			foreach(Type primitiveValueObjectType in primitiveValueObjectTypes)
			{
				mapper.RegisterType(primitiveValueObjectType,
					PrimitiveValueObjectConverter.Serialize(primitiveValueObjectType),
					PrimitiveValueObjectConverter.Deserialize(primitiveValueObjectType));
			}

			return mapper;
		}
	}
}
