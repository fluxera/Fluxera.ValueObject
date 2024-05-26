namespace Fluxera.ValueObject
{
	using System;
	using System.Collections.Concurrent;
	using System.ComponentModel;
	using System.Globalization;

	internal sealed class PrimitiveValueObjectConverter : TypeConverter
	{
		private static readonly ConcurrentDictionary<Type, TypeConverter> ActualConverters = new ConcurrentDictionary<Type, TypeConverter>();

		private readonly TypeConverter innerConverter;

		public PrimitiveValueObjectConverter(Type primitiveValueObjectType)
		{
			this.innerConverter = ActualConverters.GetOrAdd(primitiveValueObjectType, CreateActualConverter);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return this.innerConverter.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return this.innerConverter.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return this.innerConverter.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return this.innerConverter.ConvertTo(context, culture, value, destinationType);
		}

		private static TypeConverter CreateActualConverter(Type primitiveValueObjectType)
		{
			if(!primitiveValueObjectType.IsPrimitiveValueObject())
			{
				throw new InvalidOperationException($"The type '{primitiveValueObjectType}' is not a primitive value-object.");
			}

			Type valueType = primitiveValueObjectType.GetPrimitiveValueObjectValueType();
			Type actualConverterType = typeof(PrimitiveValueObjectConverter<,>).MakeGenericType(primitiveValueObjectType, valueType);
			return (TypeConverter)Activator.CreateInstance(actualConverterType);
		}
	}

	internal sealed class PrimitiveValueObjectConverter<TValueObject, TValue> : TypeConverter
		where TValueObject : PrimitiveValueObject<TValueObject, TValue>
		where TValue : IComparable
	{
		// ReSharper disable once StaticMemberInGenericType
		private static TypeConverter ValueConverter { get; } = GetIdValueConverter();

		private static TypeConverter GetIdValueConverter()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));

			if(!converter.CanConvertFrom(typeof(string)))
			{
				throw new InvalidOperationException(
					$"The type '{typeof(TValue)}' doesn't have a converter that can convert from string.");
			}

			return converter;
		}

		/// <inheritdoc />
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string)
				|| sourceType == typeof(TValue)
				|| base.CanConvertFrom(context, sourceType);
		}

		/// <inheritdoc />
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string)
				|| destinationType == typeof(TValue)
				|| base.CanConvertTo(context, destinationType);
		}

		/// <inheritdoc />
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if(value is string str)
			{
				value = ValueConverter.ConvertFrom(str);
			}

			if(value is TValue idValue)
			{
				object instance = Activator.CreateInstance(typeof(TValueObject), new object[] { idValue });
				return instance;
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <inheritdoc />
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Guard.ThrowIfNull(value);

			PrimitiveValueObject<TValueObject, TValue> primitiveValueObject = (PrimitiveValueObject<TValueObject, TValue>)value;

			TValue objectValue = primitiveValueObject.Value;
			if(destinationType == typeof(string))
			{
				return objectValue.ToString();
			}

			if(destinationType == typeof(TValue))
			{
				return objectValue;
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
