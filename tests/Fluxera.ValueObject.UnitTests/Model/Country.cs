namespace Fluxera.ValueObject.UnitTests.Model
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class Country : ValueObject<Country>
	{
		public Country(string twoLetterCode)
		{
			this.TwoLetterCode = twoLetterCode;
		}

		public string TwoLetterCode { get; }

		public static Country Create(string twoLetterCode)
		{
			return new Country(twoLetterCode);
		}
	}
}
