
using DatalagringHans.Context;
using DatalagringHans.Models;
using DatalagringHans.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatalagringHans.Services
{
	internal class AddressServices
	{
		private readonly DataContext _context = new DataContext();

		public async Task<IEnumerable<AddressEntity>> GetAllAsync()
		{
			return await _context.Address.Include(x => x.Customer).ToListAsync();
		}

		/*public async Task <AddressEntity> GetAsync(int id)
		{
			var addressEntity = await _context.Address.Include(x => x.Customer.FirstName).FirstOrDefaultAsync(x => x.Id == id);
			if(addressEntity != null)
			{
				return addressEntity;
			}
			else
			{
				return null!;
			}
		} 
		public async Task DeleteAsync(int id)
		{
			var addressEntity = await GetAsync(id); //Måste nog radera en customer först.
			if (addressEntity != null)
			{
				_context.Remove(addressEntity);
				await _context.SaveChangesAsync();
			}
		}
		*/
	}


}
