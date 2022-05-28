namespace Fluxera.ValueObject.LiteDB
{
	using System;
	using System.Reflection;
	using global::LiteDB;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class PrimitiveValueObjectConverter
	{
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
