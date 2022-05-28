namespace Fluxera.ValueObject.EntityFrameworkCore.UnitTests
{
	using System.ComponentModel.DataAnnotations;

	public class Person
	{
		[Key]
		public string Id { get; set; }

		public StringPrimitive StringPrimitive { get; set; }
	}
}
