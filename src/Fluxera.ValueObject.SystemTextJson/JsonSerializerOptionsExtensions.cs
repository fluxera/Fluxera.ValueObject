namespace Fluxera.ValueObject.SystemTextJson
{
	using System.Text.Json;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class JsonSerializerOptionsExtensions
	{
		public static void UsePrimitiveValueObject(this JsonSerializerOptions options)
		{
			options.Converters.Add(new PrimitiveValueObjectJsonConverterFactory());
		}
	}
}
