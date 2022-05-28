namespace Fluxera.ValueObject.JsonNet
{
	using Newtonsoft.Json;

	/// <summary>
	///     Extension methods for the <see cref="JsonSerializerSettings" /> type.
	/// </summary>
	public static class JsonSerializerSettingsExtensions
	{
		/// <summary>
		///     Configure the serializer to use the <see cref="PrimitiveValueObjectConverter{TValueObject,TValue}" />.
		/// </summary>
		/// <param name="settings"></param>
		public static void UsePrimitiveValueObject(this JsonSerializerSettings settings)
		{
			settings.ContractResolver = new CompositeContractResolver
			{
				new PrimitiveValueObjectContractResolver()
			};
		}
	}
}
