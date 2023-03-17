

namespace DatalagringHans.Models.Forms
{
	internal class CaseInformation
	{
		public string Description { get; set; } = null!;

		public string CustomerFirstName { get; set; } = null!;

		public string CustomerLastName { get; set; } = null!;

		public DateTime CaseCreated { get; set; } = DateTime.Now;

		public string CaseStatus { get; set; } = null!;
		
		public Guid CaseNumber { get; set; }

		public List<CommentForm>? Comments { get; set; }
	}
}
