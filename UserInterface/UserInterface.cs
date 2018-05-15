using System;
using Data;
using Domain;
using FrontEnd;

namespace FrontEnd
{
    public class UserInterface
    {
        static void ChooseTask()
        {

            bool keepMenu = true;
            while (keepMenu)
            {

                DisplayCustomersAndTravels();

                Console.WriteLine("Choose task:\n\n1. Add new customer\n2. Add new trip \n3. Registrer trip to existing customer \n4. Show Customer by Customer-ID \n");
                int i = Int32.Parse(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        AddNewCustomer(GetCustomerFromUser());
                        break;
                    case 2:
                        AddNewTravel(GetTravelFromUser());
                        break;
                    case 3:
                        RegistrerCustomerForTravel(GetRegistrationCustomerFromUser(), GetRegistrationTravelFromUser());
                        break;
                    case 4:
                        DisplayCustomer(GetCustomerIdFromUser());
                        break;
                    case 5:
                        break;
                    case 'q':
                        keepMenu = false;
                        break;
                }

            }
            public void DisplayCustomer(int customerId)
            {

                using (var context = new TravelAgencyContext())
                {

                    var customer = context.Customers.First(x => x.CustomerId == customerId);

                    Console.WriteLine($"Name: {customer.Name} \n" +
                                      $"Debt: {customer.TotalDebt}");

                    if (customer.NumberOfDebts < 3)
                        Console.WriteLine($"{customer.Name} has only {customer.NumberOfDebts} debts");
                    else
                        Console.WriteLine($"{customer.Name} has 3 debts registrered and cannot registrer for another trip");

                    // customer.Registations.Select(x => x.Travel) ForEach(x => Console.WriteLine($"{x.CustomerId,-20}{x.Name,-20}"));


                }

            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
