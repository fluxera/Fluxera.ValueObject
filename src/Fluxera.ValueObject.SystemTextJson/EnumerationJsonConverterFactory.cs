namespace Fluxera.ValueObject.SystemTextJson
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class PrimitiveValueObjectJsonConverterFactory : JsonConverterFactory
	{
		/// <inheritdoc />
		public override bool CanConvert(Type typeToConvert)
		{
			bool isPrimitiveValueObject = typeToConvert.IsPrimitiveValueObject();
			return isPrimitiveValueObject;
		}

		/// <inheritdoc />
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			Type valueType = typeToConvert.GetPrimitiveValueObjectValueType();
			Type converterTypeTemplate = typeof(PrimitiveValueObjectConverter<,>);
			Type converterType = converterTypeTemplate.MakeGenericType(typeToConvert, valueType);

			return (JsonConverter)Activator.CreateInstance(converterType);
		}
	}
}
