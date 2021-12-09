namespace Fluxera.ValueObject.UnitTests.Model
{
	using Guards;
	using JetBrains.Annotations;
	using static Guards.ExceptionHelpers;

	[PublicAPI]
	public static class GuardAgainstExtensions
	{
		public static string NonGermanIban(this IGuard guard, string input, [InvokerParameterName] string parameterName)
		{
			Guard.Against.NullOrWhiteSpace(input, nameof(input));

			if(!input.StartsWith("DE"))
			{
				throw CreateArgumentException(parameterName, "Value cannot be a non-german IBAN.");
			}

			return input;
		}
	}
}
