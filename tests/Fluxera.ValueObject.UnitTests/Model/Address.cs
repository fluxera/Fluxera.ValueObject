namespace Fluxera.ValueObject.UnitTests.Model
{
	using System.Threading.Channels;
	using Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public class Address : ValueObject<Address>
	{
		public Address(string street, string houseNumber, string postCode, string city)
		{
			Guard.Against.NullOrWhiteSpace(street, nameof(street));
			Guard.Against.NullOrWhiteSpace(houseNumber, nameof(houseNumber));
			Guard.Against.NullOrWhiteSpace(postCode, nameof(postCode));
			Guard.Against.NullOrWhiteSpace(city, nameof(city));

			this.Street = street;
			this.HouseNumber = houseNumber;
			this.PostCode = postCode;
			this.City = city;
		}

		public string Street { get; }

		public string HouseNumber { get; }

		public string PostCode { get; }

		public string City { get; }
	}
}
