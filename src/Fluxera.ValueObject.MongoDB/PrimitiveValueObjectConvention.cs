﻿namespace Fluxera.ValueObject.MongoDB
{
	using System;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class PrimitiveValueObjectConvention : ConventionBase, IMemberMapConvention
	{
		/// <inheritdoc />
		public void Apply(BsonMemberMap memberMap)
		{
			Type originalMemberType = memberMap.MemberType;
			Type memberType = Nullable.GetUnderlyingType(originalMemberType) ?? originalMemberType;

			if(memberType.IsPrimitiveValueObject())
			{
				Type valueType = memberType.GetValueType();
				Type serializerTypeTemplate = typeof(PrimitiveValueObjectSerializer<,>);
				Type serializerType = serializerTypeTemplate.MakeGenericType(memberType, valueType);

				IBsonSerializer enumerationSerializer = (IBsonSerializer)Activator.CreateInstance(serializerType);
				memberMap.SetSerializer(enumerationSerializer);
			}
		}
	}
}