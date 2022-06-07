namespace Fluxera.ValueObject.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	/// <summary>
	///     Extension methods for the <see cref="ModelBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class ModelBuilderExtensions
	{
		/// <summary>
		///     Configure the module builder to use the <see cref="PrimitiveValueObjectConverter{TValueObject,TValue}" />.
		/// </summary>
		/// <param name="modelBuilder"></param>
		public static void UsePrimitiveValueObject(this ModelBuilder modelBuilder)
		{
			Guard.Against.Null(modelBuilder, nameof(modelBuilder));

			foreach(IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
			{
				IEnumerable<PropertyInfo> properties = entityType
					.ClrType
					.GetProperties()
					.Where(type => type.PropertyType.IsPrimitiveValueObject());

				foreach(PropertyInfo property in properties)
				{
					Type enumerationType = property.PropertyType;
					Type valueType = enumerationType.GetPrimitiveValueObjectValueType();

					Type converterTypeTemplate = typeof(PrimitiveValueObjectConverter<,>);

					Type converterType = converterTypeTemplate.MakeGenericType(enumerationType, valueType);

					ValueConverter converter = (ValueConverter)Activator.CreateInstance(converterType);

					modelBuilder
						.Entity(entityType.ClrType)
						.Property(property.Name)
						.HasConversion(converter);
				}
			}
		}
	}
}
