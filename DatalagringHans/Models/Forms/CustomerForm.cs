using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalagringHans.Models.Forms
{
	internal class CustomerForm
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string Streetname { get; set; } = null!;
		public string PostalCode { get; set; } = null!;
		public string City { get; set; } = null!;
	}
}
