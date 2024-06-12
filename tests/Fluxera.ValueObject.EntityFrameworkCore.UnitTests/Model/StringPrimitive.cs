namespace Fluxera.ValueObject.EntityFrameworkCore.UnitTests.Model
{
	using Fluxera.ValueObject;

	public sealed class StringPrimitive : PrimitiveValueObject<StringPrimitive, string>
	{
		/// <inheritdoc />
		public StringPrimitive(string value) : base(value)
		{
		}
	}
}
