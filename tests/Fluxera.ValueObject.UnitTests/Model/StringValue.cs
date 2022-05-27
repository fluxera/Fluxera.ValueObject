namespace Fluxera.ValueObject.UnitTests.Model
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class StringValue : PrimitiveValueObject<StringValue, string>
	{
		/// <inheritdoc />
		public StringValue(string value) : base(value)
		{
		}
	}
}
