namespace Fluxera.ValueObject.JsonNet
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using Newtonsoft.Json.Serialization;

	/// <summary>
	///     A <see cref="IContractResolver" /> that allows to have multiple other resolver instances added.
	/// </summary>
	[PublicAPI]
	public sealed class CompositeContractResolver : IContractResolver, IEnumerable<IContractResolver>
	{
		private readonly IList<IContractResolver> contractResolvers = new List<IContractResolver>();
		private readonly DefaultContractResolver defaultContractResolver = new DefaultContractResolver();

		/// <inheritdoc />
		public JsonContract ResolveContract(Type type)
		{
			return this.contractResolvers
				.Select(x => x.ResolveContract(type))
				.FirstOrDefault();
		}

		/// <inheritdoc />
		public IEnumerator<IContractResolver> GetEnumerator()
		{
			return this.contractResolvers.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		///     Add a resolver instance.
		/// </summary>
		/// <param name="contractResolver"></param>
		public void Add(IContractResolver contractResolver)
		{
			Guard.ThrowIfNull(contractResolver);

			if(this.contractResolvers.Contains(this.defaultContractResolver))
			{
				this.contractResolvers.Remove(this.defaultContractResolver);
			}

			this.contractResolvers.Add(contractResolver);
			this.contractResolvers.Add(this.defaultContractResolver);
		}
	}
}
