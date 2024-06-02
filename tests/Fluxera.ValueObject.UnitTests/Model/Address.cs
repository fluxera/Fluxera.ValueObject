namespace Fluxera.ValueObject.UnitTests.Model
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public class Address : ValueObject<Address>, IComparable
	{
		public Address(string street, string houseNumber, string postCode, string city)
		{
			this.Street = street;
			this.HouseNumber = houseNumber;
			this.PostCode = postCode;
			this.City = city;
		}

		public string Street { get; }

		public string HouseNumber { get; }

		public string PostCode { get; }

		public string City { get; }

		/// <inheritdoc />
		public int CompareTo(object obj)
		{
			return string.Compare(this.PostCode, obj as string, StringComparison.Ordinal);
		}
	}
}
