using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatalagringHans.Context;
using DatalagringHans.Models.Entities;
using DatalagringHans.Models.Forms;
using Microsoft.EntityFrameworkCore;

namespace DatalagringHans.Services
{
	internal class CommentService
	{
		private static readonly DataContext _context = new DataContext();
		public static async Task CommentCaseEmployee(CommentForm newComment)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == newComment.Id);
			var _case = await _context.Cases.FirstOrDefaultAsync(x => x.CaseNumber == newComment.Casenumber);

			if (employee != null && _case != null)
			{
				var commentEntity = new CommentEntity
				{
					Comment = newComment.Description,
					CreatedComment = DateTime.Now,
					CaseId = newComment.Casenumber,
					EmployeeId = newComment.Id,
				};
				_context.Add(commentEntity);
				await _context.SaveChangesAsync();
			}
		}
	}
}
