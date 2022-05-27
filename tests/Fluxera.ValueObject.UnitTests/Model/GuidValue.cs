namespace Fluxera.ValueObject.UnitTests.Model
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public class GuidValue : PrimitiveValueObject<GuidValue, Guid>
	{
		/// <inheritdoc />
		public GuidValue(Guid value) : base(value)
		{
		}
	}
}
