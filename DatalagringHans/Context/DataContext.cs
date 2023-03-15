using DatalagringHans.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace DatalagringHans.Context
{

		internal class DataContext : DbContext
		{
			#region constructors and overrides
			public DataContext()
			{

			}
			public DataContext(DbContextOptions<DataContext> options) : base(options)
			{

			}

			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hanne\source\repos\DatalagringHans\DatalagringHans\Context\EntityFrameworkDatabase.mdf;Integrated Security=True;Connect Timeout=30");
			}

		#endregion

			public DbSet<AddressEntity> Address { get; set; }
			public DbSet<CustomerEntity> Customers { get; set; }
			public DbSet<CaseEntity> Cases { get; set; }
			public DbSet<CaseStatusEntity> CaseStatuses { get; set; }
			public DbSet<CommentEntity> Comments { get; set; }
			public DbSet<EmployeeEntity> Employees { get; set; }
	}
	}

