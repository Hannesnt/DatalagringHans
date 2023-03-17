

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
