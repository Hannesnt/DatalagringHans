using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatalagringHans.Context;
using DatalagringHans.Models.Forms;
using Microsoft.EntityFrameworkCore;

namespace DatalagringHans.Services
{
	internal class EmployeeService
	{
		private readonly DataContext _context = new DataContext();
		public async Task<EmployeeForm> GetSpecificEmployee(int _employeeId) 
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == _employeeId);
			if (employee != null)
			{
				return new EmployeeForm
				{
					Id= employee.Id,
					FirstName = employee.FirstName,
					LastName = employee.LastName,
					
				};
			}
			else
			{
				return null!;
			}
		}
	}
}
