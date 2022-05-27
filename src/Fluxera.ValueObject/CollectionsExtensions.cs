namespace Fluxera.ValueObject
{
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.ValueObject.Collections;
	using JetBrains.Annotations;

	/// <summary>
	///     Extensions methods for collection types.
	/// </summary>
	[PublicAPI]
	public static class CollectionsExtensions
	{
		/// <summary>
		///     Converts the given enumerable to a <see cref="ValueList{T}" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static IList<T> AsValueList<T>(this IEnumerable<T> list)
		{
			return list.ToList().AsValueList();
		}

		/// <summary>
		///     Converts the given collection to a <see cref="ValueList{T}" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static IList<T> AsValueList<T>(this ICollection<T> list)
		{
			return list.ToList().AsValueList();
		}

		/// <summary>
		///     Converts the given list to a <see cref="ValueList{T}" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static IList<T> AsValueList<T>(this IList<T> list)
		{
			return new ValueList<T>(list);
		}

		/// <summary>
		///     Converts the given set to a <see cref="ValueSet{T}" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="set"></param>
		/// <returns></returns>
		public static ISet<T> AsValueSet<T>(this ISet<T> set)
		{
			return new ValueSet<T>(set);
		}

		/// <summary>
		///     Converts the given dictionary to a <see cref="ValueDictionary{TKey,TValue}" />.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dictionary"></param>
		/// <returns></returns>
		public static IDictionary<TKey, TValue> AsValueDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			return new ValueDictionary<TKey, TValue>(dictionary);
		}
	}
}
