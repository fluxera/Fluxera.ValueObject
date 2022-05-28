namespace Fluxera.ValueObject.JsonNet
{
	using System;
	using System.Reflection;
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	[PublicAPI]
	public sealed class PrimitiveValueObjectConverter<TValueObject, TValue> : JsonConverter<TValueObject>
		where TValueObject : PrimitiveValueObject<TValueObject, TValue>
		where TValue : IComparable
	{
		/// <inheritdoc />
		public override bool CanWrite => true;

		/// <inheritdoc />
		public override bool CanRead => true;

		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, TValueObject value, JsonSerializer serializer)
		{
			if(value is null)
			{
				writer.WriteNull();
			}
			else
			{
				writer.WriteValue(value.Value);
			}
		}

		/// <inheritdoc />
		public override TValueObject ReadJson(JsonReader reader, Type objectType, TValueObject existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.Null)
			{
				return null;
			}

			TValue value = serializer.Deserialize<TValue>(reader);
			object instance = Activator.CreateInstance(objectType, BindingFlags.Public | BindingFlags.Instance, null, new object[] { value }, null);
			return (TValueObject)instance;
		}
	}
}
