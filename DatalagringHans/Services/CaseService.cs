using System.Threading.Tasks.Dataflow;
using DatalagringHans.Context;
using DatalagringHans.Models;
using DatalagringHans.Models.Entities;
using DatalagringHans.Models.Forms;
using Microsoft.EntityFrameworkCore;

namespace DatalagringHans.Services
{
	internal class CaseService
	{
		private readonly DataContext _context = new DataContext(); //Endast för vi har en tom konstruktor


		public async Task CreateCase(string description, string email)
		{
			var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
			if(customer != null)
			{
				var caseEntity = new CaseEntity
				{
					Description = description,
					CaseCreated = DateTime.Now,
					CaseStatusId = 1,
					CustomerId = customer.Id,

				};
				_context.Add(caseEntity);
				await _context.SaveChangesAsync();
			}
		}
		public async Task<IEnumerable<CaseInformation>> ShowAllCase()
		{
			var listOfCases = new List<CaseInformation>();
			foreach (var caseItem in await _context.Cases.Include(x => x.Customer).Include(x => x.CaseStatus).Include(x => x.Comment).ThenInclude(x => x.Employee).ToListAsync())
			{
				var AllCases = new CaseInformation
				{
					Description = caseItem.Description,
					CustomerFirstName = caseItem.Customer.FirstName,
					CustomerLastName = caseItem.Customer.LastName,
					CaseCreated = caseItem.CaseCreated,
					CaseStatus = caseItem.CaseStatus.Status,
					Comments = new List<CommentForm>()
				};
				if (caseItem.Comment != null)
				{
					foreach (var comment in caseItem.Comment)
					{
						AllCases.Comments.Add(new CommentForm()
						{
							Description = comment.Comment,
							CreatedComment = comment.CreatedComment,
							EmployeeFirstName = comment.Employee.FirstName,
							EmployeeLastName = comment.Employee.LastName,
						});
					}
				}

				listOfCases.Add(AllCases);
			}
			return listOfCases;
		}
		public async Task<CaseInformation> GetSpecificCase(Guid casenumber)
		{
			
			var _case = await _context.Cases.Include(x => x.Customer).Include(x => x.CaseStatus).Include(x => x.Comment).ThenInclude(x => x.Employee).FirstOrDefaultAsync(x => x.CaseNumber == casenumber);
			if(_case != null) 
			{
				var specificCase = new CaseInformation
				{
					Description = _case.Description,
					CustomerFirstName = _case.Customer.FirstName,
					CustomerLastName = _case.Customer.LastName,
					CaseCreated = _case.CaseCreated,
					CaseStatus = _case.CaseStatus.Status,
					Comments = new List<CommentForm>()
				};
				if(_case.Comment != null)
				{
					foreach(var comment in _case.Comment)
					{
						specificCase.Comments.Add(new CommentForm()
						{
							EmployeeFirstName= comment.Employee.FirstName,
							EmployeeLastName= comment.Employee.LastName,
							Description = comment.Comment,
							CreatedComment = comment.CreatedComment
						});
					}
				}
				return specificCase;
			}
			else
			{
				return null!;
			}
		}
		public async Task CommentCaseEmployee(CommentForm newComment)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == newComment.Id);
			var _case = await _context.Cases.FirstOrDefaultAsync(x => x.CaseNumber == newComment.Casenumber);

			if(employee != null && _case != null)
			{
				var commentEntity = new CommentEntity
				{
					Comment = newComment.Description,
					CreatedComment = DateTime.Now,
					CaseId = newComment.Casenumber,
					EmployeeId= newComment.Id,
				};
				_context.Add(commentEntity);
				await _context.SaveChangesAsync();
			}


		}

		public async Task ChangeCaseStatus(CaseForm caseStatus)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == caseStatus.EmployeeId);
			var _case = await _context.Cases.FirstOrDefaultAsync(x => x.CaseNumber == caseStatus.Casenumber);
			if(employee != null && _case != null)
			{
				_case.CaseStatusId = caseStatus.CaseStatusId;
				_context.Update(_case);
				await _context.SaveChangesAsync();
			}
		}
		public async Task<CaseForm> GetSpecificCaseStatus(int _statusId)
		{
			var _status = await _context.CaseStatuses.FirstOrDefaultAsync(x => x.Id == _statusId);
			if(_status != null)
			{
				return new CaseForm
				{
					CaseStatusId = _status.Id,
					CaseStatus = _status.Status
				};
			}
			else
			{
				return null!;
			}
		}
	}
}
