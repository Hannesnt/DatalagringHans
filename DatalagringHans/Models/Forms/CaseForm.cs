using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalagringHans.Models.Forms
{
	internal class CaseForm
	{
		public int EmployeeId { get; set; }
		public Guid Casenumber { get; set; }
		public string Description { get; set; } = null!;

		public string CustomerFirstName { get; set; } = null!;

		public string CustomerLastName { get; set; } = null!;

		public DateTime CaseCreated { get; set; } = DateTime.Now;

		public string CaseStatus { get; set; } = null!;

		public int CaseStatusId { get; set; }


	}
}
