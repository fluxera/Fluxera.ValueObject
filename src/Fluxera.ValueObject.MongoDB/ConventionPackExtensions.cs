namespace Fluxera.ValueObject.MongoDB
{
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="ConventionPack" /> type.
	/// </summary>
	[PublicAPI]
	public static class ConventionPackExtensions
	{
		/// <summary>
		///     Configure the serializer to use the <see cref="PrimitiveValueObjectSerializer{TValueObject,TValue}" />.
		/// </summary>
		/// <param name="pack"></param>
		/// <returns></returns>
		public static ConventionPack UsePrimitiveValueObject(this ConventionPack pack)
		{
			Guard.ThrowIfNull(pack);

			pack.Add(new PrimitiveValueObjectConvention());

			return pack;
		}
	}
}
