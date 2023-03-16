
using Microsoft.EntityFrameworkCore;
using DatalagringHans.Context;
using DatalagringHans.Models;
using DatalagringHans.Models.Entities;
using DatalagringHans.Models.Forms;

namespace DatalagringHans.Services
{
	internal class CustomerService
	{
		private static readonly DataContext _context = new DataContext();

		public static async Task CreateCustomer(CustomerForm customer) 
		{

			var customerEntity = new CustomerEntity
			{
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				PhoneNumber = customer.PhoneNumber,
			};
			var adressEntity = await _context.Address.FirstOrDefaultAsync(x => x.StreetName == customer.Streetname && x.PostalCode == customer.PostalCode && x.City == customer.City);
			if(adressEntity != null ) 
			{
				customerEntity.AddressId = adressEntity.Id;
			}
			else
			{
				customerEntity.Address = new AddressEntity 
				{
					StreetName = customer.Streetname,
					PostalCode = customer.PostalCode,
					City = customer.City,
				};
			}
			_context.Add(customerEntity);
			await _context.SaveChangesAsync();
		}
		public static async Task<IEnumerable<CustomerForm>> GetAllCustomersAsync()
		{
			var customers = new List<CustomerForm>();
			foreach(var customer in await _context.Customers.Include(x => x.Address).ToListAsync())
				customers.Add(new CustomerForm
				{
					FirstName = customer.FirstName,
					LastName = customer.LastName,
					Email = customer.Email,
					PhoneNumber	= customer.PhoneNumber,
					Streetname= customer.Address.StreetName,
					PostalCode= customer.Address.PostalCode,
					City = customer.Address.City,
				});
			return customers;
		}
		public static async Task<CustomerForm> GetSpecificCustomer(string email) 
		{
			var customer = await _context.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
			if(customer != null)
			{
				return new CustomerForm
				{
					FirstName = customer.FirstName,
					LastName = customer.LastName,
					Email = customer.Email,
					PhoneNumber = customer.PhoneNumber,
					Streetname = customer.Address.StreetName,
					PostalCode = customer.Address.PostalCode,
					City = customer.Address.City
				};
			}
			else
			{
				return null!;
			}
		}
		
	}
}
