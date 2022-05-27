namespace Fluxera.ValueObject
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     Extensions methods for the <see cref="ValueObject{TValueObject}" /> type.
	/// </summary>
	[PublicAPI]
	public static class ValueObjectExtensions
	{
		/// <summary>
		///     Checks if the given type is a <see cref="ValueObject{TValueObject}" />.
		/// </summary>
		/// <param name="type">The type to check.</param>
		/// <returns><c>true</c> if the type is a value object; <c>false</c> otherwise.</returns>
		public static bool IsValueObject(this Type type)
		{
			try
			{
				bool isSubclassOf = type.IsSubclassOfRawGeneric(typeof(ValueObject<>));
				return isSubclassOf && !type.IsInterface && !type.IsAbstract;
			}
			catch
			{
				return false;
			}
		}

		private static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
		{
			while(toCheck != null && toCheck != typeof(object))
			{
				Type cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
				if(generic == cur)
				{
					return true;
				}

				toCheck = toCheck.BaseType;
			}

			return false;
		}
	}
}
