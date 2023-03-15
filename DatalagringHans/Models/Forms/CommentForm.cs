using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalagringHans.Models.Forms
{
	internal class CommentForm
	{
		public int Id { get; set; }
		public string Description { get; set; } = null!;

		public DateTime CreatedComment { get; set; } = DateTime.Now;

		public string EmployeeFirstName { get; set; } = null!;
		public string EmployeeLastName { get; set; } = null!;
		public Guid Casenumber { get; set; } 
	}
}
