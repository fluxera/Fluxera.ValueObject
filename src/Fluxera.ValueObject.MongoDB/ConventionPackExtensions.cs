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
		public static ConventionPack UsePrimitiveValueObject(this ConventionPack pack)
		{
			pack.Add(new PrimitiveValueObjectConvention());

			return pack;
		}
	}
}
