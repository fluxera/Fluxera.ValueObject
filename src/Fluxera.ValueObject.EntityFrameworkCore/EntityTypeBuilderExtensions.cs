namespace Fluxera.ValueObject.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	/// <summary>
	///     Extension methods for the <see cref="ModelBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class EntityTypeBuilderExtensions
	{
		/// <summary>
		///     Configure the <see cref="EntityTypeBuilder" /> to use the
		///     <see cref="PrimitiveValueObjectConverter{TValueObject,TValue}" />.
		/// </summary>
		/// <param name="entityTypeBuilder"></param>
		public static void UsePrimitiveValueObject(this EntityTypeBuilder entityTypeBuilder)
		{
			Guard.ThrowIfNull(entityTypeBuilder);

			IEnumerable<PropertyInfo> properties = entityTypeBuilder.Metadata
				.ClrType
				.GetProperties()
				.Where(type => type.PropertyType.IsPrimitiveValueObject());

			foreach(PropertyInfo property in properties)
			{
				Type originalMemberType = property.PropertyType;
				Type memberType = Nullable.GetUnderlyingType(originalMemberType) ?? originalMemberType;

				if(memberType.IsPrimitiveValueObject())
				{
					Type valueType = memberType.GetPrimitiveValueObjectValueType();

					Type converterTypeTemplate = typeof(PrimitiveValueObjectConverter<,>);

					Type converterType = converterTypeTemplate.MakeGenericType(memberType, valueType);

					ValueConverter converter = (ValueConverter)Activator.CreateInstance(converterType);

					entityTypeBuilder
						.Property(property.Name)
						.HasConversion(converter);
				}
			}
		}
	}
}
