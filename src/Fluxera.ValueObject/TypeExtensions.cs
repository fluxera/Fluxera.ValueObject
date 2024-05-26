namespace Fluxera.ValueObject
{
	using System;
	using System.Collections.Generic;

	internal static class TypeExtensions
	{
		private static readonly HashSet<Type> ExtraPrimitiveTypes =
		[
			typeof(string),
			typeof(decimal),
			typeof(DateOnly),
			typeof(TimeOnly),
			typeof(DateTime),
			typeof(DateTimeOffset),
			typeof(TimeSpan),
			typeof(Guid)
		];

		/// <summary>
		///     Determines whether the specified type is a primitive. It automatically
		///     unwraps the wrapped type of the nullable.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="includeEnums">If set to <c>true</c> include enums.</param>
		/// <returns>
		///     <c>true</c> if the specified type is a primitive; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsPrimitive(this Type type, bool includeEnums = false)
		{
			type = type.UnwrapNullableType();

			if(type.IsPrimitive)
			{
				return true;
			}

			if(includeEnums && type.IsEnum)
			{
				return true;
			}

			return ExtraPrimitiveTypes.Contains(type);
		}

		/// <summary>
		///     Gets the type without nullable if the type is a <see cref="Nullable{T}" />.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		private static Type UnwrapNullableType(this Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}
	}
}
