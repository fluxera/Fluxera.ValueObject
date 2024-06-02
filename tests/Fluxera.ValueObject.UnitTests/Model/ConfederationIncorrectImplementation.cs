namespace Fluxera.ValueObject.UnitTests.Model
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	[PublicAPI]
	public class ConfederationIncorrectImplementation : ValueObject<Confederation>
	{
		public ConfederationIncorrectImplementation(string name, IList<Country> memberCountries)
		{
			this.Name = name;
			this.MemberCountries = memberCountries;
		}

		public string Name { get; }

		public IList<Country> MemberCountries { get; }
	}
}
