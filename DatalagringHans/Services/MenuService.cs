using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatalagringHans.Models;
using DatalagringHans.Models.Forms;

namespace DatalagringHans.Services
{
	internal class MenuService
	{
		private readonly CustomerService _customerService = new CustomerService();
		private readonly CaseService _caseService = new CaseService();
		private readonly EmployeeService _employeeService= new EmployeeService();
		public async Task MainMenu()
		{
			Console.Clear();
			Console.WriteLine("###### HUVUDMENY ######");
			Console.WriteLine("1. Skapa en ny användare \n2. Sök efter användare \n3. Skapa felanmälan \n4. Se allas felanmälningar \n5. Se dina felanmälningar " +
				"\n6. Kommentera case (Endast Personal) \n7. Ändra status på ärende (Endast Personal) \n \nAnge ett av följande alternativ (1-7)");
			var option = Console.ReadLine();

			switch(option)
			{
				case "1":
					await CreateUser();
					break;
				case "2":
					await GetSingleUser();
					break;
				case "3":
					await CreateNewCase();
					break;
				case "4":
					await ShowReportStatus();
					break;
				case "5":
					await ShowSpecificReport();
					break;
				case "6":
					await CommentOnCaseEmployee();
					break;
				case "7":
					await ChangeCaseStatus();
					break;
			}
			Console.ReadKey();
		}
		public async Task CreateUser()
		{
			int FormChecker = 0;
			var newCustomer = new CustomerForm();
			List<string> customerForms= new List<string>();
			Console.Clear();
			Console.WriteLine("###### Skapa Användare ######");
			Console.WriteLine("Förnamn: ");
			newCustomer.FirstName = Console.ReadLine() ?? "";
			customerForms.Add(newCustomer.FirstName);
			Console.WriteLine("Efternamn: ");
			newCustomer.LastName = Console.ReadLine() ?? "";
			customerForms.Add(newCustomer.LastName);
			Console.WriteLine("Email: ");
			newCustomer.Email = Console.ReadLine() ?? "";
			var emailCheck = await _customerService.GetSpecificCustomer(newCustomer.Email);
			if(emailCheck == null)
			{
				customerForms.Add(newCustomer.Email);
				Console.WriteLine("Telefonnummer: ");
				newCustomer.PhoneNumber = Console.ReadLine() ?? "";
				customerForms.Add(newCustomer.PhoneNumber);
				if (newCustomer.PhoneNumber.Length > 15)
				{
					Console.WriteLine("Inget gilltigt Telefonnummer!");
				}
				else
				{
					Console.WriteLine("Adress: ");
					newCustomer.Streetname = Console.ReadLine() ?? "";
					customerForms.Add(newCustomer.Streetname);
					Console.WriteLine("Postnummer: ");
					newCustomer.PostalCode = Console.ReadLine() ?? "";
					customerForms.Add(newCustomer.PostalCode);
					if (newCustomer.PostalCode.Length > 6)
					{
						Console.WriteLine("Inget gilltigt postnummer!");
					}
					else
					{
						Console.WriteLine("Stad: ");
						newCustomer.City = Console.ReadLine() ?? "";
						customerForms.Add(newCustomer.City);
						foreach (var customer in customerForms)
						{
							if (customer != "")
							{
								FormChecker++;
							}

						}
						if (FormChecker == 7)
						{
							await _customerService.CreateCustomer(newCustomer);
							Console.Clear();
							Console.WriteLine("Du är nu en registerad användare! \n\n Tryck på valfri knapp för att återgå till huvudmenyn");
						}
						else
						{
							Console.WriteLine("Du fyllde inte i formuläret korrekt");
						}

					}
				}
			}
			else
			{
				Console.WriteLine("Denna email finns redan registerad");
			}
		}

		public async Task GetSingleUser()
		{
			Console.Clear();
			Console.WriteLine("###### Sök efter användare ######");
			Console.WriteLine("Ange Email-adressen");
			string email = Console.ReadLine() ?? "";
			var customer = await _customerService.GetSpecificCustomer(email);
			if (customer != null)
			{
				Console.WriteLine($"Hittade användare: \n {customer.FirstName} {customer.LastName} \n {customer.PhoneNumber} \n {customer.Streetname} - {customer.PostalCode} - {customer.City}");
			}
			else
			{
				Console.WriteLine("Kunde inte hitta användaren.");
			}
		}
		public async Task CreateNewCase()
		{
			
			Console.Clear();
			Console.WriteLine("###### Skapa felanmälan ######");
			Console.WriteLine("Ange din Email-adressen");
			string email = Console.ReadLine() ?? "";
			var customer = await _customerService.GetSpecificCustomer(email);
			if (customer != null)
			{
				Console.WriteLine("Beskriv din felanmälan");
				string description = Console.ReadLine() ?? "";
				await _caseService.CreateCase(description, email);
				Console.Clear();
				Console.WriteLine($"Din felanmälan är nu skapt! \n\n{customer.FirstName} {customer.LastName} \n{description}\n\n Tryck på valfri knapp för att återgå till huvudmenyn");
			}
			else
			{
				Console.WriteLine("Kunde inte hitta användaren. Tryck på valfri knapp för att återgå till huvudmenyn.");
			}
			
		}
		public async Task ShowReportStatus()
		{
			Console.Clear();
			Console.WriteLine("###### Alla felanmälningar ###### \n");
			var Cases = await _caseService.ShowAllCase();
			if(Cases == null)
			{
				Console.WriteLine("Det finns inga anmälningar inne.");
			}
			else
			{
				foreach (var items in Cases)
				{
					Console.WriteLine($"#### Felanmälan skapad: {items.CaseCreated} ####");
					Console.WriteLine($"Kund: {items.CustomerFirstName} {items.CustomerLastName}");
					Console.WriteLine($"Beskrivning: {items.Description}");
					Console.WriteLine($"Status: {items.CaseStatus}");
					Console.WriteLine("Kommentarer: \n");
					if(items.Comments != null)
					{
						foreach (var comment in items.Comments)
						{
							Console.WriteLine($"{comment.EmployeeFirstName} {comment.EmployeeLastName} - {comment.CreatedComment}");
							Console.WriteLine($"{comment.Description}\n");
						}
					}
					
				}
			}
			
		}
		public async Task ShowSpecificReport()
		{
			Console.Clear();
			Console.WriteLine("###### Sök efter felanmälan ######");
			Console.WriteLine("Ange casenumret för din felanmälan:");
			if(Guid.TryParse(Console.ReadLine(), out Guid id))
			{
				var Cases = await _caseService.GetSpecificCase(id);
				if (Cases == null)
				{
					Console.Clear();
					Console.WriteLine($"Det finns ingen felanmälan kopplad till detta casenumret {id}. \nKontrollera så du skrev rätt casenummer.");
				}
				else
				{
					{
						Console.Clear();
						Console.WriteLine($"Kund: {Cases.CustomerFirstName} {Cases.CustomerLastName}");
						Console.WriteLine($"Felanmälan skapad: {Cases.CaseCreated}");
						Console.WriteLine($"Beskrivning: {Cases.Description}");
						Console.WriteLine($"Status: {Cases.CaseStatus}");
						Console.WriteLine("Kommentarer:");
						if (Cases.Comments != null)
						{
							foreach (var comment in Cases.Comments)
							{
								Console.WriteLine($"{comment.EmployeeFirstName} {comment.EmployeeLastName} - {comment.CreatedComment}");
								Console.WriteLine($"{comment.Description}\n");
							}
						}
					}
				}
			}
			else
			{
				Console.WriteLine("Det finns ingen felanmälan kopplad till detta casenumret. Kontrollera så du skrev rätt casenummer.");
			}
		}

		public async Task CommentOnCaseEmployee()
		{
			Console.Clear();
			var comment = new CommentForm();
			Console.WriteLine("Ange ditt personal id");
			if(int.TryParse(Console.ReadLine(), out int Id))
			{
				comment.Id = Id;
				var employee = await _employeeService.GetSpecificEmployee(comment.Id);
				if(employee != null)
				{
					Console.WriteLine("Ange det ordernummer du vill kommentera");
					if(Guid.TryParse(Console.ReadLine(), out Guid Orderid))
					{
						var _case = await _caseService.GetSpecificCase(Orderid);
						if(_case!= null)
						{
							comment.Casenumber = Orderid;
							Console.WriteLine("Skriv din kommentar:");
							comment.Description = Console.ReadLine() ?? "";
							await _caseService.CommentCaseEmployee(comment);
							Console.Clear();
							Console.WriteLine($"Din kommentar är nu gjord! \n\n{employee.FirstName} {employee.LastName} \n{comment.Description}\n\nTryck på valfri knapp för att återgå till huvudmenyn");
						}
						else 
						{
							Console.WriteLine($"Det finns ingen order med detta ordernummer. {Orderid}");
						}
					}
					else
					{
						Console.WriteLine($"Det finns ingen order med detta ordernummer, numrets format skall se ut såhär: {Orderid}");
					}
					
				}
				else
				{
					Console.WriteLine("Det finns ingen anställd med detta id.");
				}
			}
		}

		public async Task ChangeCaseStatus()
		{
			var _case = new CaseForm();
			Console.Clear();
			Console.WriteLine("Ange ditt personal id");
			if (int.TryParse(Console.ReadLine(), out int Id))
			{
				_case.EmployeeId = Id;
				var employee = await _employeeService.GetSpecificEmployee(Id);
				if(employee != null)
				{
					Console.WriteLine("Ange det ordernummer du vill ändra status på");
					if (Guid.TryParse(Console.ReadLine(), out Guid Orderid))
					{
						_case.Casenumber = Orderid;
						var _caseChecker = await _caseService.GetSpecificCase(Orderid);
						if(_caseChecker != null)
						{
							Console.WriteLine($"Vilken status vill du att ärendet skall ha? (1-3). Nuvarande Status: {_caseChecker.CaseStatus}\n 1. Ej påbörjad \n 2. Pågående \n 3. Avslutad");
							if (int.TryParse(Console.ReadLine(), out int StatusId))
							{
								_case.CaseStatusId = StatusId;
								var _statusChecker = await _caseService.GetSpecificCaseStatus(StatusId);
								if(_statusChecker != null)
								{
									await _caseService.ChangeCaseStatus(_case);
									Console.Clear();
									Console.WriteLine($"Din Statusändring är nu gjort. Från {_caseChecker.CaseStatus} till {_statusChecker.CaseStatus}\n\n Tryck på valfri knapp för att återgå till huvudmenyn");
								}
								else
								{
									Console.WriteLine("Du kan bara välja mellan status 1-3");
								}
							}
						}
					}
					else
					{
						Console.WriteLine($"Det finns ingen order med detta ordernummer, numrets format skall se ut såhär: {Orderid}");
					}
				}
				else
				{
					Console.WriteLine("Det finns ingen anställd med detta id.");
				}

			}
		}
	}
}
