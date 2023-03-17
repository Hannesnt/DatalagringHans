

namespace DatalagringHans.Models.Forms
{
	internal class CaseForm
	{
		public int EmployeeId { get; set; }
		public Guid Casenumber { get; set; } = Guid.NewGuid();

		public string CustomerEmail { get; set; } = null!;
		public string Description { get; set; } = null!;

		public DateTime CaseCreated { get; set; } = DateTime.Now;

		public int CaseStatusId { get; set; }

		public string? CaseStatus { get; set; }

	}
}
