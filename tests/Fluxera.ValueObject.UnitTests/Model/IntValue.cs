namespace Fluxera.ValueObject.UnitTests.Model
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class IntValue : PrimitiveValueObject<IntValue, int>
	{
		/// <inheritdoc />
		public IntValue(int value) : base(value)
		{
		}
	}
}
