namespace Fluxera.ValueObject.EntityFrameworkCore.UnitTests
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Infrastructure;

	/// <summary>
	///     Creates a unique cache key every time to prevent caching.
	/// </summary>
	public class NoModelCacheKeyFactory : IModelCacheKeyFactory
	{
		/// <inheritdoc />
		public object Create(DbContext context, bool designTime)
		{
			return Guid.NewGuid();
		}
	}
}
