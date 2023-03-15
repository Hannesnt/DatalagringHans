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
	internal class CaseStatusEntity
	{
		[Key]
		public int Id { get; set; }
		[Column(TypeName = "char(20)")]
		public string Status { get; set; } = null!;
		public ICollection<CaseEntity> Case { get; set; } = new HashSet<CaseEntity>();
	}
}
