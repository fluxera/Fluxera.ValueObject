namespace Fluxera.ValueObject
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using System.Runtime.CompilerServices;
	using System.Text;
	using JetBrains.Annotations;

	/// <summary>
	///		A base class for any value object.
	/// </summary>
	/// <typeparam name="TValueObject">The type of the value object.</typeparam>
	[PublicAPI]
	public abstract class ValueObject<TValueObject> : INotifyPropertyChanging, INotifyPropertyChanged
		where TValueObject : ValueObject<TValueObject>
	{
		/// <summary>
		///		To ensure hashcode uniqueness, a carefully selected random number multiplier
		///     is used within the calculation. 
		/// </summary>
		/// <remarks>
		///		See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
		/// </remarks>
		private const int HashMultiplier = 37;

		/// <inheritdoc />
		public event PropertyChangingEventHandler? PropertyChanging;

		/// <inheritdoc />
		public event PropertyChangedEventHandler? PropertyChanged;

		public static bool operator ==(ValueObject<TValueObject>? left, ValueObject<TValueObject>? right)
		{
			if (left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		public static bool operator !=(ValueObject<TValueObject>? left, ValueObject<TValueObject>? right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if(obj is null)
			{
				return false;
			}

			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}

			ValueObject<TValueObject>? other = obj as ValueObject<TValueObject>;
			return other != null 
				&& this.GetType() == other.GetType()
				&& this.HasSameObjectSignatureAs(other);
		}

		private bool HasSameObjectSignatureAs(ValueObject<TValueObject> other)
		{
			return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				// It is possible for two objects to return the same hash code based on
				// identically valued properties, even if they are of different types,
				// so we include the value object type in the hash calculation
				int hashCode = this.GetType().GetHashCode();

				foreach(object component in this.GetEqualityComponents())
				{
					if(component != null)
					{
						hashCode = (hashCode * HashMultiplier) ^ component.GetHashCode();
					}
				}

				return hashCode;
			}
		}

		/// <inheritdoc />
		public override string ToString()
		{
			using(IEnumerator<object> enumerator = this.GetEqualityComponents().GetEnumerator())
			{
				if(!enumerator.MoveNext())
				{
					return $"{typeof(TValueObject).Name} {{}}";
				}

				StringBuilder builder = new StringBuilder($"{typeof(TValueObject).Name} {{");
				builder.Append(enumerator.Current);
				while(enumerator.MoveNext())
				{
					builder.Append(" ,").Append(enumerator.Current);
				}
				builder.Append(" }");

				return builder.ToString();
			}
		}

		/// <summary>
		///		Gets all components of the value object that are used for equality. <br/>
		///		The default implementation get all properties via reflection. One
		///		can at any time override this behavior with a manual or custom implementation.
		/// </summary>
		/// <returns>The components to use for equality.</returns>
		protected virtual IEnumerable<object> GetEqualityComponents()
		{
			PropertyInfo[] equalityComponentsProperties = this.GetEqualityComponentsProperties();

			foreach(PropertyInfo property in equalityComponentsProperties)
			{
				object value = property.GetValue(this, null);
				yield return value;
			}
		}

		protected virtual void OnPropertyChanging(string propertyName)
		{
			this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void SetAndNotify(ref object field, object value, [CallerMemberName] string propertyName = null!)
		{
			if(field != value)
			{
				this.OnPropertyChanging(propertyName);
				field = value;
			}

			this.OnPropertyChanged(propertyName);
		}

		private PropertyInfo[] GetEqualityComponentsProperties()
		{
			// TODO: Cache the metadata.
			return this.GetType().GetProperties();
		}
	}
}
