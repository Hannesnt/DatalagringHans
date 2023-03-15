using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatalagringHans.Models.Entities
{
	[Index(nameof(Email), IsUnique = true)]
	internal class CustomerEntity
	{
		[Key]
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(50)")]
		public string FirstName { get; set; } = null!;
		[Column(TypeName = "nvarchar(50)")]
		public string LastName { get; set; } = null!;
		[Column(TypeName = "nvarchar(50)")]
		public string Email { get; set; } = null!;
		[Column(TypeName = "char(15)")]
		public string PhoneNumber { get; set; } = null!;

		public int AddressId { get; set; }
		public AddressEntity Address { get; set; } = null!;

		public ICollection<CaseEntity> Case { get; set; } = new HashSet<CaseEntity>();

	}
}
