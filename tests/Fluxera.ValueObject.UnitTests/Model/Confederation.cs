namespace Fluxera.ValueObject.UnitTests.Model
{
	using System.Collections.Generic;
	using Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public class Confederation : ValueObject<Confederation>
	{
		public Confederation(string name, IList<Country> memberCountries)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrEmpty(memberCountries, nameof(memberCountries));

			this.Name = name;
			this.MemberCountries = memberCountries.AsValueList();
		}

		public string Name { get; }

		public IList<Country> MemberCountries { get; }
	}
}
