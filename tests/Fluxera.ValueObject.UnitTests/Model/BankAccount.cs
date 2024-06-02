namespace Fluxera.ValueObject.UnitTests.Model
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class BankAccount : ValueObject<BankAccount>
	{
		public BankAccount(string name, string iban, string bic)
		{
			this.Name = name;
			this.Iban = iban;
			this.Bic = bic;
		}

		public string Name { get; }

		public string Iban { get; }

		public string Bic { get; }
	}
}
