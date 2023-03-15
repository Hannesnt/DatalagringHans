using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatalagringHans.Models.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DatalagringHans.Models.Entities
{
	internal class EmployeeEntity
	{
		[Key]
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(50)")]
		public string FirstName { get; set; } = null!;
		[Column(TypeName = "nvarchar(50)")]
		public string LastName { get; set; } = null!;

		public ICollection<CommentEntity> Comment { get; set; } = new HashSet<CommentEntity>();

	}
}
