
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatalagringHans.Models.Entities
{
	internal class AddressEntity
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(50)")]
		public string StreetName { get; set; } = null!;
		[Required]
		[Column(TypeName = "char(6)")]
		public string PostalCode { get; set; } = null!;
		[Required]
		[Column(TypeName = "nchar(20)")]
		public string City { get; set; } = null!;

		public ICollection<CustomerEntity> Customer { get; set; } = new HashSet<CustomerEntity>();
	}
	
}
