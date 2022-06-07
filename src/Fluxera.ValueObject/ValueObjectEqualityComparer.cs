namespace Fluxera.ValueObject
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A custom <see cref="IEqualityComparer{T}" /> for supporting LINQ methods such as Intersect, Union and Distinct.
	///     <br />
	///     This may be used for comparing objects of type <see cref="ValueObject{TValueObject}" />
	///     and anything that derives from it.
	/// </summary>
	/// <remarks>
	///     Microsoft decided that set operators such as Intersect, Union and Distinct should
	///     not use the IEqualityComparer Equals() method when comparing objects, but should instead
	///     use <c>GetHashCode</c> method.
	/// </remarks>
	[PublicAPI]
	public class ValueObjectEqualityComparer<TValueObject> : IEqualityComparer<TValueObject>
		where TValueObject : ValueObject<TValueObject>
	{
		/// <inheritdoc />
		public bool Equals(TValueObject firstObject, TValueObject secondObject)
		{
			if(firstObject is null && secondObject is null)
			{
				return true;
			}

			if(firstObject is null ^ secondObject is null)
			{
				return false;
			}

			return firstObject.Equals(secondObject);
		}

		/// <inheritdoc />
		public int GetHashCode(TValueObject obj)
		{
			return obj.GetHashCode();
		}
	}
}
