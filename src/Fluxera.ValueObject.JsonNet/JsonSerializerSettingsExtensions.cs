namespace Fluxera.ValueObject.JsonNet
{
	using Newtonsoft.Json;

	public static class JsonSerializerSettingsExtensions
	{
		public static void UsePrimitiveValueObject(this JsonSerializerSettings settings)
		{
			settings.ContractResolver = new CompositeContractResolver
			{
				new PrimitiveValueObjectContractResolver()
			};
		}
	}
}
