namespace Fluxera.ValueObject
{
	using System.Collections.Generic;
	using System.Linq;
	using Collections;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class CollectionsExtensions
	{
		public static IList<T> AsValueList<T>(this IEnumerable<T> list)
		{
			return list.ToList().AsValueList();
		}

		public static IList<T> AsValueList<T>(this ICollection<T> list)
		{
			return list.ToList().AsValueList();
		}

		public static IList<T> AsValueList<T>(this IList<T> list)
		{
			return new ValueList<T>(list);
		}

		public static ISet<T> AsValueSet<T>(this ISet<T> set)
		{
			return new ValueSet<T>(set);
		}

		public static IDictionary<TKey, TValue> AsValueDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			return new ValueDictionary<TKey, TValue>(dictionary);
		}
	}
}
