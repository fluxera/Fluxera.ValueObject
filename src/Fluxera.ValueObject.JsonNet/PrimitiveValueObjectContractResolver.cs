namespace Fluxera.ValueObject.JsonNet
{
	using System;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class PrimitiveValueObjectContractResolver : DefaultContractResolver
	{
		/// <inheritdoc />
		protected override JsonConverter ResolveContractConverter(Type objectType)
		{
			if(objectType.IsPrimitiveValueObject())
			{
				Type valueType = objectType.GetValueType();
				Type converterTypeTemplate = typeof(PrimitiveValueObjectConverter<,>);
				Type converterType = converterTypeTemplate.MakeGenericType(objectType, valueType);

				return (JsonConverter)Activator.CreateInstance(converterType);
			}

			return base.ResolveContractConverter(objectType);
		}
	}
}
