namespace Fluxera.ValueObject.UnitTests.Model
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	[PublicAPI]
	public class Amount : ValueObject<Amount>
	{
		public Amount(decimal quantity, Currency currency)
		{
			this.Quantity = quantity;
			this.Currency = currency;
		}

		public decimal Quantity { get; }

		public Currency Currency { get; }

		public Amount Add(Amount amount)
		{
			if(this.Currency != amount.Currency)
			{
				throw new InvalidOperationException("Cannot add amounts with different currencies.");
			}

			Amount result = new Amount(this.Quantity + amount.Quantity, this.Currency);
			return result;
		}

		/// <inheritdoc />
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Quantity;
			yield return this.Currency;
		}
	}
}
