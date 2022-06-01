namespace Fluxera.ValueObject.UnitTests
{
	using System;
	using System.ComponentModel;
	using FluentAssertions;
	using Fluxera.ValueObject.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class TypeConverterTests
	{
		[Test]
		public void ShouldConvertFromGuidStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(GuidValue));

			GuidValue result = (GuidValue)converter.ConvertFromString("4c3668ce-3aaa-4adb-bece-05baa708e20f");
			result.Should().NotBeNull();
			result.Value.Should().NotBeEmpty().And.Be("4c3668ce-3aaa-4adb-bece-05baa708e20f");
		}

		[Test]
		public void ShouldConvertFromGuidValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(GuidValue));

			GuidValue result = (GuidValue)converter.ConvertFrom(Guid.Parse("4c3668ce-3aaa-4adb-bece-05baa708e20f"));
			result.Should().NotBeNull();
			result.Value.Should().NotBeEmpty().And.Be(Guid.Parse("4c3668ce-3aaa-4adb-bece-05baa708e20f"));
		}

		[Test]
		public void ShouldConvertFromIntegerStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(IntValue));

			IntValue result = (IntValue)converter.ConvertFromString("999");
			result.Should().NotBeNull();
			result.Value.Should().Be(999);
		}

		[Test]
		public void ShouldConvertFromIntegerValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(IntValue));

			IntValue result = (IntValue)converter.ConvertFrom(999);
			result.Should().NotBeNull();
			result.Value.Should().Be(999);
		}

		[Test]
		public void ShouldConvertFromStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(StringValue));

			StringValue result = (StringValue)converter.ConvertFromString("12345");
			result.Should().NotBeNull();
			result.Value.Should().NotBeEmpty().And.Be("12345");
		}

		[Test]
		public void ShouldConvertToGuidStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(GuidValue));

			GuidValue id = new GuidValue(Guid.Parse("2ca3459d-794e-4d25-9594-bc3849972e1f"));
			string result = converter.ConvertToString(id);
			result.Should().Be("2ca3459d-794e-4d25-9594-bc3849972e1f");
		}

		[Test]
		public void ShouldConvertToGuidValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(GuidValue));

			GuidValue id = new GuidValue(Guid.Parse("2ca3459d-794e-4d25-9594-bc3849972e1f"));
			Guid result = (Guid)converter.ConvertTo(null, null, id, typeof(Guid));
			result.Should().Be(Guid.Parse("2ca3459d-794e-4d25-9594-bc3849972e1f"));
		}

		[Test]
		public void ShouldConvertToIntegerStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(IntValue));

			IntValue id = new IntValue(999);
			string result = converter.ConvertToString(id);
			result.Should().Be("999");
		}

		[Test]
		public void ShouldConvertToIntegerValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(IntValue));

			IntValue id = new IntValue(999);
			int result = (int)converter.ConvertTo(null, null, id, typeof(int));
			result.Should().Be(999);
		}

		[Test]
		public void ShouldConvertToStringValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(StringValue));

			StringValue id = new StringValue("12345");
			string result = converter.ConvertToString(id);
			result.Should().Be("12345");
		}
	}
}
