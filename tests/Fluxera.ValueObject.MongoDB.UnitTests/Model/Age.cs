namespace Fluxera.ValueObject.MongoDB.UnitTests.Model
{
	public sealed class Age : PrimitiveValueObject<Age, int>
	{
		/// <inheritdoc />
		public Age(int value) : base(value)
		{
		}
	}
}
