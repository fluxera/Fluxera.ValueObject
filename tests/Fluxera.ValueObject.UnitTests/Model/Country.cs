namespace Fluxera.ValueObject.UnitTests.Model
{
	using Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public class Country : ValueObject<Country>
	{
		public Country(string twoLetterCode)
		{
			this.TwoLetterCode = twoLetterCode;
			Guard.Against.InvalidLength(twoLetterCode, nameof(twoLetterCode), 2);
		}

		public string TwoLetterCode { get; }

		public static Country Create(string twoLetterCode)
		{
			return new Country(twoLetterCode);
		}
	}
}
