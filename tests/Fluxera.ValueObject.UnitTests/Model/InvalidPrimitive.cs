﻿namespace Fluxera.ValueObject.UnitTests.Model
{
	using JetBrains.Annotations;

	[PublicAPI]
	public class InvalidPrimitive : PrimitiveValueObject<InvalidPrimitive, Address>
	{
		/// <inheritdoc />
		public InvalidPrimitive(Address value) : base(value)
		{
		}
	}
}
