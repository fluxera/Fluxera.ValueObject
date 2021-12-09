namespace Fluxera.ValueObject.UnitTests.Model
{
	using Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public class GermanBankAccount : BankAccount
	{
		/// <inheritdoc />
		public GermanBankAccount(string name, string iban, string bic) 
			: base(name, iban, bic)
		{
			Guard.Against.NonGermanIban(iban, nameof(iban));
		}
	}
}
