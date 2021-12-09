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

		public static string InvalidLength(this IGuard guard, string input, [InvokerParameterName] string parameterName, int allowedLength)
		{
			if(input.Length != allowedLength)
			{
				throw CreateArgumentException(parameterName, $"Value cannot be other than {allowedLength} characters in length.");
			}

			return input;
		}
	}
}
