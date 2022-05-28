namespace Fluxera.ValueObject.JsonNet.UnitTests
{
	public sealed class StringPrimitive : PrimitiveValueObject<StringPrimitive, string>
	{
		/// <inheritdoc />
		public StringPrimitive(string value) : base(value)
		{
		}
	}
}
