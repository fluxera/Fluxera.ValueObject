namespace Fluxera.ValueObject.MongoDB
{
	using System;
	using System.Reflection;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using JetBrains.Annotations;

	/// <summary>
	///     A serializer that handles instances of primitive value objects.
	/// </summary>
	/// <typeparam name="TValueObject"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[PublicAPI]
	public sealed class PrimitiveValueObjectSerializer<TValueObject, TValue> : SerializerBase<TValueObject>
		where TValueObject : PrimitiveValueObject<TValueObject, TValue>
		where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
	{
		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TValueObject value)
		{
			if(value is null)
			{
				context.Writer.WriteNull();
			}
			else
			{
				BsonSerializer.Serialize(context.Writer, value.Value);
			}
		}

		/// <inheritdoc />
		public override TValueObject Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			if(context.Reader.CurrentBsonType == BsonType.Null)
			{
				context.Reader.ReadNull();
				return null;
			}

			TValue value = BsonSerializer.Deserialize<TValue>(context.Reader);
			object instance = Activator.CreateInstance(args.NominalType, BindingFlags.Public | BindingFlags.Instance, null, [value], null);
			return (TValueObject)instance;
		}
	}
}
