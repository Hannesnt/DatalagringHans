using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DatalagringHans.Models.Entities
{
	internal class CaseEntity
	{
		[Key]
		[Column(TypeName = "uniqueidentifier")]
		public Guid CaseNumber { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(max)")]
		public string Description { get; set; } = null!;
		[Required]
		[Column(TypeName = "datetime")]
		public DateTime CaseCreated { get; set; }

		public int CustomerId { get; set; }
		public CustomerEntity Customer { get; set; } = null!;
		public int CaseStatusId { get; set; }
		public CaseStatusEntity CaseStatus { get; set; } = null!;

		public ICollection<CommentEntity> Comment { get; set; } = new HashSet<CommentEntity>();

	}
}
