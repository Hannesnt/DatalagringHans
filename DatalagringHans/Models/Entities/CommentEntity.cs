using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DatalagringHans.Models.Entities
{
	internal class CommentEntity
	{
			[Key]
			public int Id { get; set; }
			[Required]
			[Column(TypeName = "nvarchar(max)")]
			public string Comment { get; set; } = null!;
			public Guid CaseId { get; set; }
			public CaseEntity Case { get; set; } = null!;
			public DateTime CreatedComment { get; set; }
			public int EmployeeId { get; set; }
			public EmployeeEntity Employee { get; set; } = null!;

	}
}
