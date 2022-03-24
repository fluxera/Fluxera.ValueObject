namespace Fluxera.ValueObject.UnitTests.Model
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class PostCode : PrimitiveValueObject<PostCode, string>
	{
		public PostCode(string value)
		{
			this.Value = value;
		}

		public string WillNotBeConsideredForEqualityAndHashCode { get; set; }
	}
}
