namespace Fluxera.ValueObject.UnitTests.Model
{
	using System.Collections.Generic;
	using Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public class ConfederationIncorrectImplementation : ValueObject<Confederation>
	{
		public ConfederationIncorrectImplementation(string name, IList<Country> memberCountries)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrEmpty(memberCountries, nameof(memberCountries));

			this.Name = name;
			this.MemberCountries = memberCountries;
		}

		public string Name { get; }

		public IList<Country> MemberCountries { get; }
	}
}
