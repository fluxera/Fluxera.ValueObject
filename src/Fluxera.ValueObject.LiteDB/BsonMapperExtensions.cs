namespace Fluxera.ValueObject.LiteDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="BsonMapper" /> type.
	/// </summary>
	[PublicAPI]
	public static class BsonMapperExtensions
	{
		/// <summary>
		///     Configure the mapper to use the <see cref="PrimitiveValueObjectConverter" />.
		/// </summary>
		/// <param name="mapper"></param>
		/// <returns></returns>
		public static BsonMapper UsePrimitiveValueObject(this BsonMapper mapper)
		{
			Guard.ThrowIfNull(mapper);

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
