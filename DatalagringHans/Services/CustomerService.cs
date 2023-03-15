
using Microsoft.EntityFrameworkCore;
using DatalagringHans.Context;
using DatalagringHans.Models;
using DatalagringHans.Models.Entities;
using DatalagringHans.Models.Forms;

namespace DatalagringHans.Services
{
	internal class CustomerService
	{
		private readonly DataContext _context = new DataContext(); //Endast för vi har en tom konstruktor

		public async Task CreateCustomer(CustomerForm customer) //alltid async await när det kommer till databaser
		{

			var customerEntity = new CustomerEntity
			{
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				PhoneNumber = customer.PhoneNumber,
			};
			var adressEntity = await _context.Address.FirstOrDefaultAsync(x => x.StreetName == customer.Streetname && x.PostalCode == customer.PostalCode && x.City == customer.City);
			if(adressEntity != null ) //Kolla ifall adressen redan finns
			{
				customerEntity.AddressId = adressEntity.Id;
			}
			else
			{
				customerEntity.Address = new AddressEntity //Annars adda en ny adress
				{
					StreetName = customer.Streetname,
					PostalCode = customer.PostalCode,
					City = customer.City,
				};
			}
			_context.Add(customerEntity);
			await _context.SaveChangesAsync();
		}
		public async Task<IEnumerable<CustomerForm>> GetAllCustomersAsync()
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
		public async Task<CustomerForm> GetSpecificCustomer(string email) //Expression func?  
		/*    public async Task<AddressEntity> GetAsync(Expression<Func<AddressEntity, bool>> predicate)
{
	var item = await _context.Addresses.Include(x => x.Users).FirstOrDefaultAsync(predicate);

	if (item != null)
	{
		return item;
	}

	return null!;

			internal abstract class GenericServiec<TEntity> where TEntity : class
				{
				private read only DataContext _context = new Datacontext;
				
				public async Task<Ienumerable<Tentity>> GetAllAsync(){
				return await _context.Set<TEntity>().ToListAsync;
				}

}*/
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
