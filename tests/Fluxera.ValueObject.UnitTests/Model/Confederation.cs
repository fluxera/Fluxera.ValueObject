namespace Fluxera.ValueObject.UnitTests.Model
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	[PublicAPI]
	public class Confederation : ValueObject<Confederation>
	{
		public Confederation(string name, IList<Country> memberCountries)
		{
			this.Name = name;
			this.MemberCountries = memberCountries.AsValueList();
		}

		public string Name { get; }

		public IList<Country> MemberCountries { get; }
	}
}
