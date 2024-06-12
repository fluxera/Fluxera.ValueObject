namespace Fluxera.ValueObject.EntityFrameworkCore.UnitTests.Model
{
	using System.ComponentModel.DataAnnotations;
	using Fluxera.ValueObject.LiteDB.UnitTests.Model;

	public class Person
	{
		[Key]
		public string Id { get; set; }

		public string Name { get; set; }

		public Age Age { get; set; }

		public StringPrimitive StringPrimitive { get; set; }
	}
}
