namespace Fluxera.ValueObject.UnitTests.Model
{
	using Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public class BankAccount : ValueObject<BankAccount>
	{
		public BankAccount(string name, string iban, string bic)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(iban, nameof(iban));
			Guard.Against.NullOrWhiteSpace(bic, nameof(bic));

			this.Name = name;
			this.Iban = iban;
			this.Bic = bic;
		}

		public string Name { get; }

		public string Iban { get; }

		public string Bic { get; }
	}
}
