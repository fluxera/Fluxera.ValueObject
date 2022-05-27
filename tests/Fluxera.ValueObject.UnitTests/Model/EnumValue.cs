namespace Fluxera.ValueObject.UnitTests.Model
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class EnumValue : PrimitiveValueObject<EnumValue, Currency>
	{
		/// <inheritdoc />
		public EnumValue(Currency value) : base(value)
		{
		}
	}
}
