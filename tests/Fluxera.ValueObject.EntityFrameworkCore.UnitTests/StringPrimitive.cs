namespace Fluxera.ValueObject.EntityFrameworkCore.UnitTests
{
	public sealed class StringPrimitive : PrimitiveValueObject<StringPrimitive, string>
	{
		/// <inheritdoc />
		public StringPrimitive(string value) : base(value)
		{
		}
	}
}
