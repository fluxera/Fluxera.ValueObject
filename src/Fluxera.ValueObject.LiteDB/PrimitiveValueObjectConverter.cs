namespace Fluxera.ValueObject.LiteDB
{
	using System;
	using System.Reflection;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     A converter for primitive value objects.
	/// </summary>
	[PublicAPI]
	public static class PrimitiveValueObjectConverter
	{
		/// <summary>
		///     Serialize the given primitive value object instance.
		/// </summary>
		/// <param name="primitiveValueObjectType"></param>
		/// <returns></returns>
		public static Func<object, BsonValue> Serialize(Type primitiveValueObjectType)
		{
			return obj =>
			{
				PropertyInfo property = primitiveValueObjectType.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
				object value = property?.GetValue(obj);

				BsonValue bsonValue = new BsonValue(value);
				return bsonValue;
			};
		}

		/// <summary>
		///     Deserialize a primitive value object instance from the given bson value.
		/// </summary>
		/// <param name="primitiveValueObjectType"></param>
		/// <returns></returns>
		public static Func<BsonValue, object> Deserialize(Type primitiveValueObjectType)
		{
			return bson =>
			{
				if(bson.IsNull)
				{
					return null;
				}

				object value = bson.RawValue;
				object instance = Activator.CreateInstance(primitiveValueObjectType, BindingFlags.Public | BindingFlags.Instance, null, new object[] { value }, null);
				return instance;
			};
		}
	}
}
