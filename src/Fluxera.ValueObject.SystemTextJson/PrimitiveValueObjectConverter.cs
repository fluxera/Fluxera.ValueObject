namespace Fluxera.ValueObject.SystemTextJson
{
	using System;
	using System.Reflection;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class PrimitiveValueObjectConverter<TValueObject, TValue> : JsonConverter<TValueObject>
		where TValueObject : PrimitiveValueObject<TValueObject, TValue>
		where TValue : IComparable
	{
		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, TValueObject value, JsonSerializerOptions options)
		{
			if(value is null)
			{
				writer.WriteNullValue();
			}
			else
			{
				JsonSerializer.Serialize(writer, value.Value, options);
			}
		}

		/// <inheritdoc />
		public override TValueObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}

			TValue value = JsonSerializer.Deserialize<TValue>(ref reader, options);
			object instance = Activator.CreateInstance(typeToConvert, BindingFlags.Public | BindingFlags.Instance, null, new object[] { value }, null);
			return (TValueObject)instance;
		}
	}
}
