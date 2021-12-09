namespace Fluxera.ValueObject
{
	using System;
	using System.Collections.Concurrent;
	using System.Linq;
	using System.Reflection;
	using Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class PropertyAccessor
	{
		private static readonly ConcurrentDictionary<Type, PropertyAccessor[]> propertyAccessorsMap = new ConcurrentDictionary<Type, PropertyAccessor[]>();

		private static readonly MethodInfo callInnerDelegateMethod = typeof(PropertyAccessor).GetMethod(nameof(CallInnerDelegate), BindingFlags.NonPublic | BindingFlags.Static)!;

		private PropertyAccessor(string propertyName, Func<object, object> getterFunc)
		{
			Guard.Against.NullOrWhiteSpace(propertyName, nameof(propertyName));
			Guard.Against.Null(getterFunc, nameof(getterFunc));

			this.PropertyName = propertyName;
			this.GetterFunc = getterFunc;
		}

		public string PropertyName { get; }

		private Func<object, object> GetterFunc { get; }

		public object? Invoke(object target)
		{
			return this.GetterFunc.Invoke(target);
		}

		public static PropertyAccessor[] GetPropertyAccessors(Type type)
		{
			return propertyAccessorsMap
				.GetOrAdd(type, _ => type
					.GetProperties()
					.Select(property =>
					{
						MethodInfo? getMethod = property.GetMethod;
						Type? declaringType = property.DeclaringType;
						Type propertyType = property.PropertyType;

						Type getMethodDelegateType = typeof(Func<,>).MakeGenericType(declaringType, propertyType);
						Delegate? getMethodDelegate = getMethod.CreateDelegate(getMethodDelegateType);
						MethodInfo callInnerGenericMethodWithTypes = callInnerDelegateMethod.MakeGenericMethod(declaringType, propertyType);
						Func<object, object> getter = (Func<object, object>)callInnerGenericMethodWithTypes.Invoke(null, new object[] { getMethodDelegate });

						return new PropertyAccessor(property.Name, getter);
					}).ToArray());
		}

		// Called via reflection.
		private static Func<object, object> CallInnerDelegate<T, TResult>(Func<T, TResult> func)
		{
			return instance => func.Invoke((T)instance);
		}
	}
}
