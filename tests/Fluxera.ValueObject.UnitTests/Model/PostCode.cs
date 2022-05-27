namespace Fluxera.ValueObject.UnitTests.Model
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class PostCode : PrimitiveValueObject<PostCode, string>
	{
		public PostCode(string value) : base(value)
		{
		}

		public string WillNotBeConsideredForEqualityAndHashCode { get; set; }
	}
}
